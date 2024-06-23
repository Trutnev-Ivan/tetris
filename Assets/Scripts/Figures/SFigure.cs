using DefaultNamespace;
using tetris.Figures.Enum;
using UnityEngine;

namespace tetris.Figures
{
    public class SFigure: Figure
    {
        public SFigure(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int centerX = Settings.instance.getCountTileX() / 2;
            int _maxY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();
            
            tiles[0] = new Tile(centerX, _maxY, startCoords);
            tiles[1] = new Tile(centerX + 1, _maxY, startCoords);
            tiles[2] = new Tile(centerX, _maxY - 1, startCoords);
            tiles[3] = new Tile(centerX - 1, _maxY - 1, startCoords);
            
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
            int _minX = minX;

            if (maxX == Settings.instance.getCountTileX() - 1)
            {
                _minX--;
            }
            
            tiles[0].setPosition(_minX, maxY - 1);
            tiles[1].setPosition(_minX + 1, maxY - 1);
            tiles[2].setPosition(_minX + 1, maxY);
            tiles[3].setPosition(_minX + 2, maxY);
        }

        protected void rotateToVertical()
        {
            int _maxY = maxY;

            if (minY == 0)
            {
                _maxY++;
            }
            
            tiles[0].setPosition(minX, _maxY);
            tiles[1].setPosition(minX, _maxY - 1);
            tiles[2].setPosition(minX + 1, _maxY - 1);
            tiles[3].setPosition(minX + 1, _maxY - 2);
        }
    }
}