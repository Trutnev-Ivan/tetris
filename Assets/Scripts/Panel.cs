using UnityEngine;
using tetris;

[RequireComponent(typeof(SpriteRenderer))]
public class Panel : MonoBehaviour
{
    void Start()
    {
        setPanelSize();
        drawTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void setPanelSize()
    {
        transform.position = new Vector3(0, 0, 100);
        transform.localScale = new Vector3(Settings.instance.getWidth(), Settings.instance.getHeight(), 0);
    }
    
    protected void drawTiles()
    {
        int countX = Settings.instance.getCountTileX();
        int countY = Settings.instance.getCountTileY();
        float offset = Settings.instance.getOffset();
        
        float startX = transform.position.x - transform.localScale.x / 2 + offset;
        float startY = transform.position.y - transform.localScale.y / 2 + offset;
        
        for (int y = 0; y < countY; ++y)
        {
            for (int x = 0; x < countX; ++x)
            {
                SpriteRenderer tile = Settings.instance.getTile();
                
                float _x = startX + tile.size.x / 2 + x * (tile.size.x + offset);
                float _y = startY + tile.size.y / 2 + y * (tile.size.y + offset);
                
                tile.transform.position = new Vector3(_x, _y, 1);
                tile.color = Settings.instance.getPanelTileColor();
            }
        }
    }
}
