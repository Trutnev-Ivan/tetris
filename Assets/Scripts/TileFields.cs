using tetris.Figures;

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
    }
}