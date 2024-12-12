using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Creators
{
    public class GridCreator
    {
        public List<Vector2> Create(int width, int height, float cellSize)
        {
            var cellsPositions = new List<Vector2>();

            var fullWidth = cellSize * width;
            var fullHeight = cellSize * height;

            var startPosX = -(fullWidth / 2f) + cellSize / 2f;
            var startPosY = -(fullHeight / 2f) + cellSize / 2f;

            for (int xCoord = 0; xCoord < width; xCoord++)
            {
                for (int yCoord = 0; yCoord < height; yCoord++)
                {
                    var posX = startPosX + xCoord * cellSize;
                    var posY = startPosY + yCoord * cellSize;

                    cellsPositions.Add(new Vector2(posX,posY));
                }
            }

            return cellsPositions;
        }
    }
}
