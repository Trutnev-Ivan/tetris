using UnityEngine;

namespace DefaultNamespace
{
    public class ColorFabric
    {
        private static Color[] colors =
        {
            Color.green,
            Color.blue,
            Color.cyan,
            Color.red,
            Color.yellow
        };

        public static Color getRandomColor()
        {
            return colors[Random.Range(0, colors.Length - 1)];
        }
    }
}