using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Creators.Interfaces;
using Gameplay.Data;
using Gameplay.Helpers;
using Gameplay.Levels.Data;
using Gameplay.Managers;
using Gameplay.Managers.Interfaces;
using Gameplay.Spawners;
using Gameplay.Views;
using UnityEngine;
using VContainer;

namespace Gameplay.Creators
{
    [RequireComponent(typeof(CategoryItemCellsSpawner), typeof(CategoryItemsSpawner),typeof(GameFieldBounds))]
    public class GameFieldCreator : MonoBehaviour, ICategoryItemsCreator
    {
        public event Action<List<CategoryItemData>> ItemsCreated;
        
        [SerializeField] private CategoryItemsSpawner _categoryItemsSpawner;
        [SerializeField] private CategoryItemCellsSpawner _categoryItemCellsSpawner;

        [SerializeField] private GameFieldBounds _gameFieldBounds;
        [SerializeField] private Transform _gameFieldParent; //used to clear and scale manipulations
        private ILevelStarter _levelStarter;
        
        [Inject]
        public void Construct(ILevelStarter levelStarter)
        {
            _levelStarter = levelStarter;
            _levelStarter.LevelStarted += CreateField;
        }
        
        private void CreateField(LevelData levelData, bool firstLaunch)
        {
            ResetGameField();

            var cells= SpawnCells(levelData, out var cellsPositions);
            AdjustFieldSizeToFitScreen(cellsPositions);
            
            SpawnItemsInCells(levelData, cells, firstLaunch);
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

        private void SpawnItemsInCells(LevelData levelData, List<CategoryItemCellView> cells, bool showCellsAnimated)
        {
            var items = _categoryItemsSpawner.SpawnRandomMany(levelData.Category,
                cells.Select(x=>x.transform).ToList(), _categoryItemCellsSpawner.CellSize);

            for (var i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.SetRelatedItemView(items[i]);
                cell.Show(showCellsAnimated);
            }
            
            ItemsCreated?.Invoke(items.Select(x=>x.RelatedData).ToList());
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
        
        private void OnDisable()
        {
            _levelStarter.LevelStarted -= CreateField;
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