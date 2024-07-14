using tetris.Figures;
using UnityEngine;

namespace tetris
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

        public static bool hasFigureIntersection(Figure figure)
        {
            bool isIntersected = false;

            foreach (Tile tile in figure)
            {
                isIntersected |= hasTile(tile.Row, tile.Col);
            }

            return isIntersected;
        }

        public static void clean()
        {
            for (int i = 0; i < Settings.instance.getCountTileX(); ++i)
            {
                for (int j = 0; j < Settings.instance.getCountTileY(); ++j)
                {
                    if (tiles[i,j] != null)
                    {
                        tiles[i,j].delete();
                        tiles[i,j] = null;
                    }
                }
            }
        }
    }
}