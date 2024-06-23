using UnityEngine;

namespace tetris.Figures
{
    public class Tile
    {
        private SpriteRenderer tile;
        private Vector2 startCoords;
        private int col;
        private int row;

        public Tile(int row, int col, Vector2 startCoords)
        {
            tile = Settings.instance.getTile();
            this.startCoords = startCoords;
            setPosition(row, col);
        }

        public Tile(Vector2 startCoords)
        {
            tile = Settings.instance.getTile();
            this.startCoords = startCoords;
            setPosition(0, 0);
        }

        public void setColor(Color color)
        {
            tile.color = color;
        }
        
        public void setPosition(int row, int col)
        {
            Row = row;
            Col = col;
            calcPosition();
        }
        
        protected void calcPosition()
        {
            float offset = Settings.instance.getOffset();
            float x = startCoords.x + tile.size.x / 2 + Row * (tile.size.x + offset);
            float y = startCoords.y + tile.size.y / 2 + Col * (tile.size.y + offset);
            tile.transform.position = new Vector3(x, y, 1);
        }

        public int Col
        {
            get => col;
            set
            {
                col = value;
                
                if (value > Settings.instance.getCountTileY() - 1)
                {
                    col = Settings.instance.getCountTileY() - 1;
                }
                else if (value < 0)
                {
                    col = 0;
                }
            }
        }

        public int Row
        {
            get => row;
            set
            {
                row = value;

                if (value > Settings.instance.getCountTileX() - 1)
                {
                    row = Settings.instance.getCountTileX() - 1;
                }
                else if (value < 0)
                {
                    row = 0;
                }
            }
        }

        public void delete()
        {
            GameObject.Destroy(tile);
            tile = null;
        }
    }
}