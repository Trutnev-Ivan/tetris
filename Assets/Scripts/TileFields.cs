using tetris.Figures;
using UnityEngine;

namespace tetris
{
    public class TileFields
    {
        protected static Tile[,] tiles = new Tile[Settings.instance.getCountTileX(), Settings.instance.getCountTileY()];

        public static bool hasTile(int x, int y)
        {
            return tiles[x, y] != null;
        }

        public static void deleteTile(int x, int y)
        {
            tiles[x, y].delete();
            tiles[x, y] = null;
        }

        public static void addTile(Tile tile)
        {
            tiles[tile.X, tile.Y] = tile;
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
                isIntersected |= hasTile(tile.X, tile.Y);
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

        public static void moveBottomHigherThan(int col)
        {
            for (int colUpper = col + 1; colUpper < Settings.instance.getCountTileY(); ++colUpper)
            {
                for (int row = 0; row < Settings.instance.getCountTileX(); ++row)
                {
                    if (hasTile(row, colUpper))
                    {
                        moveTile(new Vector2Int(row, colUpper), new Vector2Int(row, colUpper - 1));
                    }
                }
            }
        }
    }
}