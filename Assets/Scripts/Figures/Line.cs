using DefaultNamespace;
using tetris.Figures.Enum;
using UnityEngine;

namespace tetris.Figures
{
    public class Line: Figure
    {
        public Line(Vector2 startCoords): base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            Color color = ColorFabric.getRandomColor();
            
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i] = new Tile(
                    Settings.instance.getCountTileX() / 2, 
                    Settings.instance.getCountTileY() - i - 1, 
                    startCoords);
                tiles[i].setColor(color);
            }
        }

        protected override bool canRotate()
        {
            switch (rotateState)
            {
                case RotateState.TOP:
                case RotateState.BOTTOM:
                    return canRotateToHorizontal();
                case RotateState.LEFT:
                case RotateState.RIGHT:
                    return canRotateToVertical();
            }

            return base.canRotate();
        }

        protected bool canRotateToHorizontal()
        {
            int startX = getStartXToHorizontalRotation();

            for (int i = 0; i < COUNT_TILES; ++i)
            {
                if (TileFields.hasTile(startX + i, maxY))
                {
                    return false;
                }
            }

            return true;
        }

        protected bool canRotateToVertical()
        {
            int startY = getStartYToVerticalRotation();
                
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                if (TileFields.hasTile(maxX, startY + i))
                {
                    return false;
                }
            }

            return true;
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

        private int getStartXToHorizontalRotation()
        {
            int startX = maxX - COUNT_TILES + 1;
                
            if (startX < 0)
            {
                startX = 0;
            }
            else if (startX > Settings.instance.getCountTileX() - COUNT_TILES)
            {
                startX = Settings.instance.getCountTileX() - COUNT_TILES;
            }

            return startX;
        }
        
        protected void rotateToHorizontal()
        {
            int startX = getStartXToHorizontalRotation();

            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i].setPosition(startX + i, maxY);
            }
        }

        private int getStartYToVerticalRotation()
        {
            int startY = maxY - COUNT_TILES + 1;

            if (startY < 0)
            {
                startY = 0;
            }
            else if (startY > Settings.instance.getCountTileY() - COUNT_TILES)
            {
                startY = Settings.instance.getCountTileY() - COUNT_TILES;
            }

            return startY;
        }
        
        protected void rotateToVertical()
        {
            int startY = getStartYToVerticalRotation();
                
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i].setPosition(maxX, startY + i);
            }            
        }
    }
}