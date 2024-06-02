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
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i] = new Tile(
                    Settings.instance.getCountTileX() / 2, 
                    Settings.instance.getCountTileY() - i - 1, 
                    startCoords);
                tiles[i].setColor(Color.green);
            }
        }

        protected override void rotateFigure()
        {
            if (maxX == minX)
            {
                rotateToHorisontal();
            }
            else
            {
                rotateToVertical();
            }
        }

        protected void rotateToHorisontal()
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

            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i].setPosition(startX + i, maxY);
            }            
        }

        protected void rotateToVertical()
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
                
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i].setPosition(maxX, startY + i);
            }            
        }
    }
}