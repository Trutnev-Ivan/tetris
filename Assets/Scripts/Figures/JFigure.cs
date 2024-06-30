using System;
using DefaultNamespace;
using UnityEngine;
using tetris.Figures.Enum;

namespace tetris.Figures
{
    public class JFigure: Figure
    {
        public JFigure(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int centerX = Settings.instance.getCountTileX() / 2;
            int maxY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();

            tiles[0] = new Tile(centerX, maxY, startCoords);
            tiles[1] = new Tile(centerX, maxY - 1, startCoords);
            tiles[2] = new Tile(centerX, maxY  - 2, startCoords);
            tiles[3] = new Tile(centerX - 1, maxY - 2, startCoords);
            
            tiles[0].setColor(color);
            tiles[1].setColor(color);
            tiles[2].setColor(color);
            tiles[3].setColor(color);
        }

        protected override bool canRotate()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                    return canRotateToTiles(getTilesTopRotate());
                case RotateState.BOTTOM:
                    return canRotateToTiles(getTilesBottomRotate());
                case RotateState.LEFT:
                    return canRotateToTiles(getTilesLeftRotate());
                case RotateState.RIGHT:
                    return canRotateToTiles(getTilesRightRotate());
            }

            return base.canRotate();
        }
        
        protected override void rotateFigure()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                    setNewRotatedTiles(getTilesTopRotate());
                    break;
                case RotateState.BOTTOM:
                    setNewRotatedTiles(getTilesBottomRotate());
                    break;
                case RotateState.LEFT:
                    setNewRotatedTiles(getTilesLeftRotate());
                    break;
                case RotateState.RIGHT:
                    setNewRotatedTiles(getTilesRightRotate());
                    break;
            }
        }

        protected void setNewRotatedTiles(Vector2Int[] vectors)
        {
            for (int i = 0; i < vectors.Length; ++i)
            {
                tiles[i].setPosition(vectors[i].x, vectors[i].y);                
            }
        }

        protected bool canRotateToTiles(Vector2Int[] vectors)
        {
            foreach (Vector2Int vector in vectors)
            {
                if (TileFields.hasTile(vector.x, vector.y))
                {
                    return false;
                }
            }
            
            return true;
        }

        private Vector2Int[] getTilesTopRotate()
        {
            int _maxY = maxY;

            if (minY < 3)
            {
                _maxY = 2;
            }

            return new Vector2Int[]
            {
                new Vector2Int(minX, _maxY),
                new Vector2Int(minX, _maxY - 1),
                new Vector2Int(minX, _maxY - 2),
                new Vector2Int(minX + 1, _maxY)
            };
        }

        private Vector2Int[] getTilesBottomRotate()
        {
            int _maxY = maxY;

            if (minY < 3)
            {
                _maxY = 2;
            }
            
            return new Vector2Int[]
            {
                new Vector2Int(maxX, _maxY),
                new Vector2Int(maxX, _maxY - 1),
                new Vector2Int(maxX, _maxY - 2),
                new Vector2Int(maxX - 1, _maxY)
            };
        }

        protected Vector2Int[] getTilesLeftRotate()
        {
            int _minX = minX;

            if (Math.Abs(maxX - Settings.instance.getCountTileX() - 1) <= 3)
            {
                _minX = Settings.instance.getCountTileX() - 3;
            }
            
            return new Vector2Int[]{
                new Vector2Int(_minX, maxY - 1),
                new Vector2Int(_minX + 1, maxY - 1),
                new Vector2Int(_minX + 2, maxY - 1),
                new Vector2Int(_minX, maxY),
            };
        }

        protected Vector2Int[] getTilesRightRotate()
        {
            int _maxX = maxX;

            if (minX < 3)
            {
                _maxX = 2;
            }
            
            return new Vector2Int[]{
                new Vector2Int(_maxX - 2, maxY),
                new Vector2Int(_maxX - 1, maxY),
                new Vector2Int(_maxX, maxY),
                new Vector2Int(_maxX, maxY - 1),
            };
        }
    }
}