using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Enums;
using Gameplay.Helpers;
using Gameplay.Levels.Data;
using Gameplay.Managers;
using Gameplay.Spawners;
using UnityEngine;

namespace Gameplay.Creators
{
    [RequireComponent(typeof(CategoryItemCellsSpawner), typeof(CategoryItemsSpawner),typeof(GameFieldBounds))]
    public class GameFieldCreator : MonoBehaviour
    {
        [SerializeField] private CategoryItemsSpawner _categoryItemsSpawner;
        [SerializeField] private CategoryItemCellsSpawner _categoryItemCellsSpawner;

        [SerializeField] private GameFieldBounds _gameFieldBounds;
        [SerializeField] private Transform _gameFieldParent; //used to clear and scale manipulations
        

        private void CreateField(LevelData levelData)
        {
            TryResetGameField();
            
            var cellsSize = _categoryItemCellsSpawner.CellSize;
            var cellsPositions = new GridCreator().Create(levelData.GridSize.x, levelData.GridSize.y, cellsSize.x); //assume that squared cell, but adjustable to height/width
            foreach (var cellPosition in cellsPositions)
            {
                var itemCell = _categoryItemCellsSpawner.Spawn(cellPosition, _gameFieldParent);
                var item =_categoryItemsSpawner.SpawnRandom(levelData.Category, itemCell.transform, parentScaleToFit: cellsSize);
                itemCell.SetRelatedItemView(item);
            }
            
            AdjustFieldSizeToFitScreen(levelData.GridSize,cellsPositions, cellsSize);
        }

        private void AdjustFieldSizeToFitScreen(Vector2 gridSize, List<Vector2> cellsPositions, Vector2 cellSize)
        {
            var cam = Camera.main;

            var screenPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            
            if (gridSize.x>gridSize.y) //correct width
            {
                var maxX = screenPoint.x - _gameFieldBounds.BordersOffset.x;
                var maxCellX = cellsPositions.Max(cell => cell.x) + cellSize.x / 2f;
                _gameFieldParent.localScale = (Vector3.one * maxX/maxCellX);
            }
            else //correct height
            {
                var maxY = screenPoint.y -_gameFieldBounds.BordersOffset.y; 
                var maxCellY = cellsPositions.Max(cell => cell.y) + cellSize.y / 2f;
                _gameFieldParent.localScale = (Vector3.one * maxY/maxCellY);
            }
        }

        private void TryResetGameField()
        {
       
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