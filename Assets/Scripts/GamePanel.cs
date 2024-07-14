using UnityEngine;
using tetris.Figures;
using tetris;

[RequireComponent(typeof(SpriteRenderer))]
public class GamePanel : MonoBehaviour
{
    FigureActionManager figureActionManager;

    void Start()
    {
        setPanelSize();
        drawTiles();
        
        figureActionManager = new FigureActionManager(getStartCoords());
        startGame();

        RestartGameEvent.Instance.AddListener(TileFields.clean);
        RestartGameEvent.Instance.AddListener(startGame);
        FinishedGameEvent.Instance.AddListener(finishGame);
    }

    protected void finishGame()
    {
        StopCoroutine(figureActionManager.getMoveBottomCoroutine());
    }

    protected void startGame()
    {
        figureActionManager.instanceFigure();
        StartCoroutine(figureActionManager.getMoveBottomCoroutine());
    }

    public void Update()
    {
        figureActionManager.update();
    }
    
    public void FixedUpdate()
    {
        figureActionManager.FixedUpdate();
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

    public Vector2 getStartCoords()
    {
        float offset = Settings.instance.getOffset();
        
        return new Vector2(
            transform.position.x - transform.localScale.x / 2 + offset,
            transform.position.y - transform.localScale.y / 2 + offset
        );
    }
}
