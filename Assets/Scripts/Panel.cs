using UnityEngine;
using tetris;
using tetris.Figures;

[RequireComponent(typeof(SpriteRenderer))]
public class Panel : MonoBehaviour
{
    protected bool isLeftMove = false;
    protected bool isRightMove = false;

    protected bool isLeftPressed = false;
    protected bool isRightPressed = false;
    
    private Figure figure;
    
    void Start()
    {
        setPanelSize();
        drawTiles();
        figure = new LFigure(getStartCoords());
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            figure.rotate();
        }
        
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                figure.moveRight();
                isRightPressed = true;
            }
        
            if (Input.GetAxis("Horizontal") < 0)
            {
                figure.moveLeft();
                isLeftPressed = true;
            }
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            isRightPressed = false;
            isLeftPressed = false;
        }
        
        isRightMove = false;
        isLeftMove = false;
        
        if (isRightPressed && Input.GetButton("Fire3"))
        {
            isRightMove = true;
        }
        
        if (isLeftPressed && Input.GetButton("Fire3"))
        {
            isLeftMove = true;
        }
    }
    
    public void FixedUpdate()
    {
        if (isRightMove)
        {
            figure.moveRight();
        }
        else if (isLeftMove)
        {
            figure.moveLeft();
        }
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

        Vector2 startCoords = getStartCoords();
        
        for (int y = 0; y < countY; ++y)
        {
            for (int x = 0; x < countX; ++x)
            {
                Tile tile = new Tile(x, y, startCoords);
                tile.setColor(Settings.instance.getPanelTileColor());
            }
        }
    }

    protected Vector2 getStartCoords()
    {
        float offset = Settings.instance.getOffset();
        
        return new Vector2(
            transform.position.x - transform.localScale.x / 2 + offset,
            transform.position.y - transform.localScale.y / 2 + offset
        );
    }
}
