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
                    rotateToHorizontal();
                    break;
                case RotateState.LEFT:
                case RotateState.RIGHT:
                    rotateToVertical();
                    break;
            }
        }

        protected void rotateToHorizontal()
        {
            int _maxX = maxX;

            if (minX == 0)
            {
                _maxX++;
            }
            
            tiles[0].setPosition(_maxX - 2, maxY);
            tiles[1].setPosition(_maxX - 1, maxY);
            tiles[2].setPosition(_maxX - 1, maxY - 1);
            tiles[3].setPosition(_maxX, maxY - 1);
        }

        protected void rotateToVertical()
        {
            int centerX = minX + 1;
            int _maxY = maxY;

            if (minY == 0)
            {
                _maxY--;
            }
            
            tiles[0].setPosition(centerX, _maxY - 1);
            tiles[1].setPosition(centerX, _maxY - 2);
            tiles[2].setPosition(centerX + 1, _maxY);
            tiles[3].setPosition(centerX + 1, _maxY - 1);
        }
    }
}