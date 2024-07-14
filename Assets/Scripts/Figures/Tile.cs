using UnityEngine;

namespace tetris.Figures
{
    public class Tile
    {
        private SpriteRenderer tile;
        private Vector2 startCoords;
        private int x;
        private int y;

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
        
        public void setPosition(int x, int y)
        {
            X = x;
            Y = y;
            calcPosition();
        }
        
        protected void calcPosition()
        {
            float offset = Settings.instance.getOffset();
            float x = startCoords.x + tile.size.x / 2 + X * (tile.size.x + offset);
            float y = startCoords.y + tile.size.y / 2 + Y * (tile.size.y + offset);
            tile.transform.position = new Vector3(x, y, 1);
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                
                if (value > Settings.instance.getCountTileY() - 1)
                {
                    y = Settings.instance.getCountTileY() - 1;
                }
                else if (value < 0)
                {
                    y = 0;
                }
            }
        }

        public int X
        {
            get => x;
            set
            {
                x = value;

                if (value > Settings.instance.getCountTileX() - 1)
                {
                    x = Settings.instance.getCountTileX() - 1;
                }
                else if (value < 0)
                {
                    x = 0;
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