using System;
using DefaultNamespace;
using tetris.Figures.Enum;
using UnityEngine;

namespace tetris.Figures
{
    public class LFigure: Figure
    {
        public LFigure(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int centerX = Settings.instance.getCountTileX() / 2;
            int maxY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();

            tiles[0] = new Tile(centerX, maxY, startCoords);
            tiles[1] = new Tile(centerX, maxY - 1, startCoords);
            tiles[2] = new Tile(centerX, maxY - 2, startCoords);
            tiles[3] = new Tile(centerX + 1, maxY - 2, startCoords);

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

        protected Vector2Int[] getTilesTopRotate()
        {
            int _maxY = maxY;

            if (minY <= 3)
            {
                _maxY = Settings.instance.getCountTileY() - 3;
            }
            
            return new Vector2Int[]{
                new Vector2Int(minX, _maxY),
                new Vector2Int(minX + 1, _maxY),
                new Vector2Int(minX + 1, _maxY - 1),
                new Vector2Int(minX + 1, _maxY - 2)
            };
        }

        protected Vector2Int[] getTilesBottomRotate()
        {
            int _maxY = maxY;
            
            if (minY <= 3)
            {
                _maxY = Settings.instance.getCountTileY() - 3;
            }
            
            return new Vector2Int[]{
                new Vector2Int(minX, _maxY),
                new Vector2Int(minX, _maxY - 1),
                new Vector2Int(minX, _maxY - 2),
                new Vector2Int(minX + 1, _maxY - 2)
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
                new Vector2Int(_minX, maxY),
                new Vector2Int(_minX + 1, maxY),
                new Vector2Int(_minX + 2, maxY),
                new Vector2Int(_minX, maxY - 1)
            };
        }

        protected Vector2Int[] getTilesRightRotate()
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
                new Vector2Int(_minX + 2, maxY)
            };
        }
    }
}