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

        protected override void rotateFigure()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                    rotateToTop();
                    break;
                case RotateState.RIGHT:
                    rotateToRight();
                    break;
                case RotateState.BOTTOM:
                    rotateToBottom();
                    break;
                case RotateState.LEFT:
                    rotateToLeft();
                    break;
            }
        }

        protected void rotateToLeft()
        {
            int centerX = minX + 1;
            int _minY = maxY;

            if (minY == 0)
            {
                _minY++;
            }
            
            tiles[0].setPosition(centerX, _minY);
            tiles[1].setPosition(centerX, _minY - 1);
            tiles[2].setPosition(centerX, _minY - 2);
            tiles[3].setPosition(centerX - 1, _minY - 1);
        }
        
        protected void rotateToTop()
        {
            int centerY = maxY - 1;
            int _minX = minX;

            if (maxX == Settings.instance.getCountTileX() - 1)
            {
                _minX--;
            }
            
            tiles[0].setPosition(_minX, centerY);
            tiles[1].setPosition(_minX + 1, centerY);
            tiles[2].setPosition(_minX + 2, centerY);
            tiles[3].setPosition(_minX + 1, centerY + 1);
        }
        
        protected void rotateToRight()
        {
            int centerX = minX + 1;
            int _maxY = maxY;

            if (minY == 0)
            {
                _maxY++;
            }
            
            tiles[0].setPosition(centerX, _maxY);
            tiles[1].setPosition(centerX, _maxY - 1);
            tiles[2].setPosition(centerX, _maxY - 2);
            tiles[3].setPosition(centerX + 1, _maxY - 1);
        }
        
        protected void rotateToBottom()
        {
            int _maxX = maxX;

            if (minX == 0)
            {
                _maxX++;
            }
            
            tiles[0].setPosition(_maxX - 2, maxY);
            tiles[1].setPosition(_maxX - 1, maxY);
            tiles[2].setPosition(_maxX, maxY);
            tiles[3].setPosition(_maxX - 1, maxY - 1);
        }
    }
}