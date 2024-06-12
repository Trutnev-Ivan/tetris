using UnityEditor;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private SpriteRenderer tile;
    [SerializeField] private Color panelTileColor; // Цвет тайла в панели
    [SerializeField] [Range(4, 100)] private int countTileX; // Кол-во тайлов по оси X  
    [SerializeField] [Range(4, 100)] private int countTileY; // Кол-во тайлов по оси Y
    [SerializeField] [Range(0, 10)] private float offset; // Место между тайлами
    [SerializeField] [Range(1, 3000)] private int width;
    [SerializeField] [Range(1, 3000)] private int height;

    private static Settings _instance;

    public static Settings instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<Settings>();
            }

            return _instance;
        }
    }
    
    public float getWidth()
    {
        return countTileX * getTileSize().x + (countTileX + 1) * offset;
    }

    public float getHeight()
    {
        return countTileY * getTileSize().y + (countTileY + 1) * offset;
    }

    public SpriteRenderer getTile()
    {
        Vector2 size = getTileSize();

        SpriteRenderer _tile = Instantiate(tile, new Vector3(1, 1, 1), Quaternion.identity);
        _tile.size = size;
        _tile.transform.localScale = new Vector3(1, 1, 1);

        return _tile;
    }

    public Vector2 getTileSize()
    {
        float x = (width - getOffset() * (getCountTileX() + 1)) / getCountTileX();
        float y = (height - getOffset() * (getCountTileY() + 1)) / getCountTileY();

        if (x > y)
        {
            x = y;
        }
        else
        {
            y = x;
        }

        return new Vector2(x, y);
    }

    public Color getPanelTileColor()
    {
        return panelTileColor;
    }

    public int getCountTileX()
    {
        return countTileX;
    }

    public int getCountTileY()
    {
        return countTileY;
    }

    public float getOffset()
    {
        return offset;
    }
}