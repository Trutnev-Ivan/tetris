using System.Collections;
using UnityEngine;
using tetris.Figures.Enum;
using tetris.Events;

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
        protected RotateState rotateState = RotateState.BOTTOM;
        
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
            if (canRotate())
            {
                rotateState = rotateState.next();
            
                rotateFigure();
                calcMinMaxCoords();
            }
        }

        protected virtual bool canRotate()
        {
            return true;
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
                if (tile.X > maxX)
                {
                    maxX = tile.X;
                }
                
                if (tile.X < minX)
                {
                    minX = tile.X;
                }

                if (tile.Y > maxY)
                {
                    maxY = tile.Y;
                }
                
                if (tile.Y < minY)
                {
                    minY = tile.Y;
                }
            }
        }

        protected virtual bool canMoveLeft()
        {
            bool canMove = minX > 0;

            if (!canMove)
            {
                return false;
            }
            
            foreach (Tile tile in tiles)
            {
                if (TileFields.hasTile(tile.X - 1, tile.Y))
                {
                    return false;
                }
            }

            return true;
        } 
        
        public void moveLeft()
        {
            if (canMoveLeft())
            {
                for (int i = 0; i < COUNT_TILES; ++i)
                {
                    tiles[i].setPosition(tiles[i].X - 1, tiles[i].Y);
                }

                --minX;
                --maxX;
            }
        }

        protected virtual bool canMoveRight()
        {
            bool canMove = maxX < Settings.instance.getCountTileX() - 1;

            if (!canMove)
            {
                return false;
            }

            foreach (Tile tile in tiles)
            {
                if (TileFields.hasTile(tile.X + 1, tile.Y))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void moveRight()
        {
            if (canMoveRight())
            {
                for (int i = 0; i < COUNT_TILES; ++i)
                {
                    tiles[i].setPosition(tiles[i].X + 1, tiles[i].Y);
                }

                ++minX;
                ++maxX;
            }
        }

        protected virtual bool canMoveBottom()
        {
            bool canMove = minY > 0;

            if (!canMove)
            {
                return false;
            }
            
            foreach (Tile tile in tiles)
            {
                if (TileFields.hasTile(tile.X, tile.Y - 1))
                {
                    return false;
                }
            }

            return true;
        }
        
        public void moveBottom()
        {
            if (!canMoveBottom())
            {
                FinishedMoveBottomEvent.Instance.Invoke();
                return;
            }
            
            for (int i = 0; i < COUNT_TILES; ++i)
            {
                tiles[i].setPosition(tiles[i].X, tiles[i].Y - 1);
            }

            --minY;
            --maxY;
        }
        
        public IEnumerator GetEnumerator()
        {
            return tiles.GetEnumerator();
        }

        public void delete()
        {
            foreach (Tile tile in this)
            {
                tile.delete();
            }
        }
    }
}