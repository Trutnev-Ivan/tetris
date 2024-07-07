using tetris.Figures;
using UnityEngine;

namespace DefaultNamespace
{
    public class TileFields
    {
        protected static Tile[,] tiles = new Tile[Settings.instance.getCountTileX(), Settings.instance.getCountTileY()];

        public static bool hasTile(int row, int col)
        {
            return tiles[row, col] != null;
        }

        public static void deleteTile(int row, int col)
        {
            tiles[row, col].delete();
            tiles[row, col] = null;
        }

        public static void addTile(Tile tile)
        {
            tiles[tile.Row, tile.Col] = tile;
        }

        public static void moveTile(Vector2Int oldCoords, Vector2Int newCoords)
        {
            tiles[oldCoords.x, oldCoords.y].setPosition(newCoords.x, newCoords.y);
            tiles[newCoords.x, newCoords.y] = tiles[oldCoords.x, oldCoords.y];
            tiles[oldCoords.x, oldCoords.y] = null;
        }
    }
}