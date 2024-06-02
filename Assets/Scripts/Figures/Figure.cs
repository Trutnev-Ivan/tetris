using System;
using UnityEngine;

namespace tetris.Figures
{
    public class Figure
    {
        public const int COUNT_TILES = 4;
        protected Tile[] tiles = new Tile[COUNT_TILES];
        protected Vector2 startCoords;
        protected int maxX = 0;
        protected int minX = Settings.instance.getCountTileX() - 1;
        protected int maxY = 0;
        protected int minY = Settings.instance.getCountTileY() - 1;
        
        public Figure(Vector2 startCoords)
        {
            this.startCoords = startCoords;
            initTiles();
            calcMinMaxCoords();
        }

        protected virtual void initTiles()
        {
        }

        public void rotate()
        {
            rotateFigure();
            calcMinMaxCoords();
        }

        protected virtual void rotateFigure()
        {
        }

        protected void calcMinMaxCoords()
        {
            maxX = 0;
            minX = Settings.instance.getCountTileX() - 1;
            maxY = 0;
            minY = Settings.instance.getCountTileY() - 1;
            
            foreach (Tile tile in tiles)
            {
                if (tile.Row > maxX)
                {
                    maxX = tile.Row;
                }
                
                if (tile.Row < minX)
                {
                    minX = tile.Row;
                }

                if (tile.Col > maxY)
                {
                    maxY = tile.Col;
                }
                
                if (tile.Col < minY)
                {
                    minY = tile.Col;
                }
            }
        }

        public void moveLeft()
        {
            if (minX > 0)
            {
                for (int i = 0; i < COUNT_TILES; ++i)
                {
                    tiles[i].setPosition(tiles[i].Row - 1, tiles[i].Col);
                }

                --minX;
                --maxX;
            }
        }

        public void moveRight()
        {
            if (maxX < Settings.instance.getCountTileX() - 1)
            {
                for (int i = 0; i < COUNT_TILES; ++i)
                {
                    tiles[i].setPosition(tiles[i].Row + 1, tiles[i].Col);
                }

                ++minX;
                ++maxX;
            }
        }
    }
}