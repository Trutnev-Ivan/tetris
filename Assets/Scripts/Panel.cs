using System.Collections;
using UnityEngine;
using tetris.Figures;
using tetris;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Panel : MonoBehaviour
{
    protected bool isLeftMove = false;
    protected bool isRightMove = false;
    protected bool isLeftPressed = false;
    protected bool isRightPressed = false;
    private Figure figure;
    private UnityAction finishedMoveBottomAction;
    
    void Start()
    {
        setPanelSize();
        drawTiles();
        figure = FigureFabric.instanceFigure(getStartCoords());
        finishedMoveBottomAction = new UnityAction(finishedCallback);
        
        Figure.finishedMoveBottom.AddListener(finishedMoveBottomAction);

        StartCoroutine(moveBottomFigure());
    }

    private void finishedCallback()
    {
        HashSet<int> checkColsToFill = new HashSet<int>();
        HashSet<int> colsUpperMoveToBottom = new HashSet<int>();

        foreach (Tile tile in figure)
        {
            TileFields.addTile(tile);
            checkColsToFill.Add(tile.Col);
        }
        
        // TODO: в отдельный метод (или класс)
        foreach (int col in checkColsToFill)
        {
            bool needDeleteRow = true;

            for (int row = 0; row < Settings.instance.getCountTileX(); ++row)
            {
                needDeleteRow &= TileFields.hasTile(row, col);

                if (!needDeleteRow)
                {
                    break;
                }
            }

            if (needDeleteRow)
            {
                colsUpperMoveToBottom.Add(col);

                for (int row = 0; row < Settings.instance.getCountTileX(); ++row)
                {
                    TileFields.deleteTile(row, col);
                }   
            }
        }

        if (colsUpperMoveToBottom.Count > 0)
        {
            ScoreText.changedScoreEvent.Invoke(colsUpperMoveToBottom.Count);
        }

        // Перемещаем строки вниз
        foreach (int col in colsUpperMoveToBottom)
        {
            for (int colUpper = col + 1; colUpper < Settings.instance.getCountTileY(); ++colUpper)
            {
                for (int row = 0; row < Settings.instance.getCountTileX(); ++row)
                {
                    if (TileFields.hasTile(row, colUpper))
                    {
                        TileFields.moveTile(new Vector2Int(row, colUpper), new Vector2Int(row, colUpper - 1));
                    }
                }
            }
        }

        figure = FigureFabric.instanceFigure(getStartCoords());

        if (TileFields.hasFigureIntersection(figure))
        {
            Debug.Log("Intersected");
            StopCoroutine(moveBottomFigure());
            Figure.finishedMoveBottom.RemoveListener(finishedMoveBottomAction);
            figure.delete();
        }
    }
    
    IEnumerator moveBottomFigure()
    {
        for (;;)
        {
            if (figure is not null)
            {
                figure.moveBottom();   
            }
            
            yield return new WaitForSeconds(0.2f);
        }
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
        
        if (isRightPressed && Input.GetKey(KeyCode.LeftShift))
        {
            isRightMove = true;
        }

        if (isLeftPressed && Input.GetKey(KeyCode.LeftShift))
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

    public Vector2 getStartCoords()
    {
        float offset = Settings.instance.getOffset();
        
        return new Vector2(
            transform.position.x - transform.localScale.x / 2 + offset,
            transform.position.y - transform.localScale.y / 2 + offset
        );
    }
}
