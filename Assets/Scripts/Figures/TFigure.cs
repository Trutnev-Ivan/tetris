using DefaultNamespace;
using tetris.Figures.Enum;
using UnityEngine;

namespace tetris.Figures
{
    public class TFigure: Figure
    {
        
        public TFigure(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int startX = Settings.instance.getCountTileX() / 2;
            int startY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();
            
            tiles[0] = new Tile(
                startX - 1, 
                startY, 
                startCoords);
            tiles[1] = new Tile(
                startX, 
                startY, 
                startCoords);
            tiles[2] = new Tile(
                startX + 1, 
                startY, 
                startCoords);
            tiles[3] = new Tile(
                startX, 
                startY - 1, 
                startCoords);
            
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

        protected Vector2Int[] getTilesLeftRotate()
        {
            int centerX = minX + 1;
            int _minY = maxY;

            if (minY == 0)
            {
                _minY++;
            }
            
            return new Vector2Int[]{
                new Vector2Int(centerX, _minY),
                new Vector2Int(centerX, _minY - 1),
                new Vector2Int(centerX, _minY - 2),
                new Vector2Int(centerX - 1, _minY - 1)
            };
        }
        
        protected Vector2Int[] getTilesTopRotate()
        {
            int centerY = maxY - 1;
            int _minX = minX;

            if (maxX == Settings.instance.getCountTileX() - 1)
            {
                _minX--;
            }
            
            return new Vector2Int[]{
                new Vector2Int(_minX, centerY),
                new Vector2Int(_minX + 1, centerY),
                new Vector2Int(_minX + 2, centerY),
                new Vector2Int(_minX + 1, centerY + 1)
            };
        }
        
        protected Vector2Int[] getTilesRightRotate()
        {
            int centerX = minX + 1;
            int _maxY = maxY;

            if (minY == 0)
            {
                _maxY++;
            }
            
            if (maxX == Settings.instance.getCountTileX() - 1)
            {
                --centerX;
            }

            return new Vector2Int[]{
                new Vector2Int(centerX, _maxY),
                new Vector2Int(centerX, _maxY - 1),
                new Vector2Int(centerX, _maxY - 2),
                new Vector2Int(centerX + 1, _maxY - 1)
            };
        }
        
        protected Vector2Int[] getTilesBottomRotate()
        {
            int _maxX = maxX;

            if (minX == 0)
            {
                _maxX++;
            }
            
            return new Vector2Int[]{
                new Vector2Int(_maxX - 2, maxY),
                new Vector2Int(_maxX - 1, maxY),
                new Vector2Int(_maxX, maxY),
                new Vector2Int(_maxX - 1, maxY - 1)
            };
        }
    }
}