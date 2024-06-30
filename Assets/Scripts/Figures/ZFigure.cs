using DefaultNamespace;
using tetris.Figures.Enum;
using UnityEngine;

namespace tetris.Figures
{
    public class ZFigure: Figure
    {
        public ZFigure(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int centerX = Settings.instance.getCountTileX() / 2;
            int maxY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();

            tiles[0] = new Tile(centerX - 1, maxY, startCoords);
            tiles[1] = new Tile(centerX, maxY, startCoords);
            tiles[2] = new Tile(centerX, maxY - 1, startCoords);
            tiles[3] = new Tile(centerX + 1, maxY - 1, startCoords);
            
            tiles[0].setColor(color);
            tiles[1].setColor(color);
            tiles[2].setColor(color);
            tiles[3].setColor(color);
        }
        
        protected override void rotateFigure()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                case RotateState.BOTTOM:
                    setNewRotatedTiles(getTilesHorizontalRotate());
                    break;
                case RotateState.LEFT:
                case RotateState.RIGHT:
                    setNewRotatedTiles(getTilesVerticalRotate());
                    break;
            }
        }

        protected override bool canRotate()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                case RotateState.BOTTOM:
                    return canRotateToTiles(getTilesHorizontalRotate());
                case RotateState.LEFT:
                case RotateState.RIGHT:
                    return canRotateToTiles(getTilesVerticalRotate());
            }

            return base.canRotate();
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

        protected Vector2Int[] getTilesHorizontalRotate()
        {
            int _maxX = maxX;

            if (minX == 0)
            {
                _maxX++;
            }
            
            return new Vector2Int[]{
                new Vector2Int(_maxX - 2, maxY),
                new Vector2Int(_maxX - 1, maxY),
                new Vector2Int(_maxX - 1, maxY - 1),
                new Vector2Int(_maxX, maxY - 1)
            };
        }

        protected Vector2Int[] getTilesVerticalRotate()
        {
            int centerX = minX + 1;
            int _maxY = maxY;

            if (minY == 0)
            {
                _maxY--;
            }
            
            if (maxX == Settings.instance.getCountTileX() - 1)
            {
                --centerX;
            }

            return new Vector2Int[]{
                new Vector2Int(centerX, _maxY - 1),
                new Vector2Int(centerX, _maxY - 2),
                new Vector2Int(centerX + 1, _maxY),
                new Vector2Int(centerX + 1, _maxY - 1)
            };
        }
    }
}