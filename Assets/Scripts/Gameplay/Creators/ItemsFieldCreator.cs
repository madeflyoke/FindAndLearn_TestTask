using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Creators.Interfaces;
using Gameplay.Data;
using Gameplay.Helpers;
using Gameplay.Levels.Data;
using Gameplay.Spawners;
using Gameplay.Views;
using UnityEngine;

namespace Gameplay.Creators
{
    [RequireComponent(typeof(CategoryItemCellsSpawner), typeof(CategoryItemsSpawner),typeof(GameFieldBounds))]
    public class ItemsFieldCreator : MonoBehaviour, ILevelItemsFieldCreator
    {
        [SerializeField] private CategoryItemsSpawner _categoryItemsSpawner;
        [SerializeField] private CategoryItemCellsSpawner _categoryItemCellsSpawner;

        [SerializeField] private GameFieldBounds _gameFieldBounds;
        [SerializeField] private Transform _gameFieldParent; //used to clear and scale manipulations

        public void Create(LevelData levelData, List<CategoryItemData> itemsData, bool animated)
        {
            ResetGameField();

            var cells= SpawnCells(levelData, out var cellsPositions);
            
            SpawnItemsInCells(itemsData, cells, animated);
            AdjustFieldSizeToFitScreen(cellsPositions);
        }

        private List<CategoryItemCellView> SpawnCells(LevelData levelData, out List<Vector2> cellsPositions)
        {
            var result = new List<CategoryItemCellView>();
            cellsPositions = new GridCreator()
                .Create(levelData.GridSize.x, levelData.GridSize.y, _categoryItemCellsSpawner.CellSize.x); //assume that squared cell, but adjustable to height/width

            foreach (var cellPosition in cellsPositions)
            {
                result.Add(_categoryItemCellsSpawner.Spawn(cellPosition, _gameFieldParent));
            }

            return result;
        }

        private void SpawnItemsInCells(List<CategoryItemData> itemsData, List<CategoryItemCellView> cells, bool showCellsAnimated)
        {
            var items = _categoryItemsSpawner.SpawnItems(itemsData,
                cells.Select(x=>x.transform).ToList(), _categoryItemCellsSpawner.CellSize);

            for (var i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.SetRelatedItemView(items[i]);
                cell.Show(showCellsAnimated);
            }
        }

        private void AdjustFieldSizeToFitScreen(List<Vector2> cellsPositions)
        {
            var cam = Camera.main;

            var screenPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            
            var maxCellX = cellsPositions.Max(cell => cell.x) + _categoryItemCellsSpawner.CellSize.x / 2f;
            var maxCellY = cellsPositions.Max(cell => cell.y) + _categoryItemCellsSpawner.CellSize.y / 2f;

            var availableWidth = screenPoint.x - _gameFieldBounds.BordersOffset.x;
            var availableHeight = screenPoint.y - _gameFieldBounds.BordersOffset.y;

            var scaleX = availableWidth / maxCellX;
            var scaleY = availableHeight / maxCellY;

            var scale = Mathf.Min(scaleX, scaleY);

            _gameFieldParent.localScale = new Vector3(scale, scale, 1);
        }

        private void ResetGameField()
        {
            for (int i = _gameFieldParent.childCount-1; i >=0 ; i--)
            {
               Destroy(_gameFieldParent.GetChild(i).gameObject);
            }
            _gameFieldParent.localScale = Vector3.one;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            _categoryItemsSpawner ??= GetComponent<CategoryItemsSpawner>();
            _categoryItemCellsSpawner ??= GetComponent<CategoryItemCellsSpawner>();
            _gameFieldBounds ??= GetComponent<GameFieldBounds>();
        }

#endif
    }
}