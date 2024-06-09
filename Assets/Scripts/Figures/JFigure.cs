using System;
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
            Color color = Color.green;

            tiles[0] = new Tile(centerX, maxY, startCoords);
            tiles[1] = new Tile(centerX, maxY - 1, startCoords);
            tiles[2] = new Tile(centerX, maxY  - 2, startCoords);
            tiles[3] = new Tile(centerX - 1, maxY - 2, startCoords);
            
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
                case RotateState.BOTTOM:
                    rotateToBottom();
                    break;
                case RotateState.LEFT:
                    rotateToLeft();
                    break;
                case RotateState.RIGHT:
                    rotateToRight();
                    break;
            }
        }

        protected void rotateToTop()
        {
            int _maxY = maxY;

            if (minY < 3)
            {
                _maxY = 2;
            }
            
            tiles[0].setPosition(minX, _maxY);
            tiles[1].setPosition(minX, _maxY - 1);
            tiles[2].setPosition(minX, _maxY - 2);
            tiles[3].setPosition(minX + 1, _maxY);
        }

        protected void rotateToBottom()
        {
            int _maxY = maxY;

            if (minY < 3)
            {
                _maxY = 2;
            }
            
            tiles[0].setPosition(maxX, _maxY);
            tiles[1].setPosition(maxX, _maxY - 1);
            tiles[2].setPosition(maxX, _maxY - 2);
            tiles[3].setPosition(maxX - 1, _maxY);
        }

        protected void rotateToLeft()
        {
            int _minX = minX;

            if (Math.Abs(maxX - Settings.instance.getCountTileX() - 1) <= 3)
            {
                _minX = Settings.instance.getCountTileX() - 3;
            }
            
            tiles[0].setPosition(_minX, maxY - 1);
            tiles[1].setPosition(_minX + 1, maxY - 1);
            tiles[2].setPosition(_minX + 2, maxY - 1);
            tiles[3].setPosition(_minX, maxY);
        }

        protected void rotateToRight()
        {
            int _maxX = maxX;

            if (minX < 3)
            {
                _maxX = 2;
            }
            
            tiles[0].setPosition(_maxX - 2, maxY);
            tiles[1].setPosition(_maxX - 1, maxY);
            tiles[2].setPosition(_maxX, maxY);
            tiles[3].setPosition(_maxX, maxY - 1);
        }
    }
}