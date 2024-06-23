using DefaultNamespace;
using UnityEngine;

namespace tetris.Figures
{
    public class Square: Figure
    {
        public Square(Vector2 startCoords) : base(startCoords)
        {
            
        }

        protected override void initTiles()
        {
            int startX = Settings.instance.getCountTileX() / 2;
            int startY = Settings.instance.getCountTileY() - 1;
            Color color = ColorFabric.getRandomColor();
            
            tiles[0] = new Tile(
                startX,
                startY,
                startCoords);
            tiles[0].setColor(color);
            
            tiles[1] = new Tile(
                startX + 1,
                startY,
                startCoords);
            tiles[1].setColor(color);
            
            tiles[2] = new Tile(
                startX,
                startY - 1,
                startCoords);
            tiles[2].setColor(color);
            
            tiles[3] = new Tile(
                startX + 1,
                startY - 1,
                startCoords);
            tiles[3].setColor(color);
        }
    }
}