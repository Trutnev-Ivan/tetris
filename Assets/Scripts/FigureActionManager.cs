using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tetris.Figures;
using tetris.Events;
using tetris;

public class FigureActionManager
{
    protected bool isLeftMove = false;
    protected bool isRightMove = false;
    protected bool isLeftPressed = false;
    protected bool isRightPressed = false;
    protected bool isDownArrowPressed = false;
    private Figure figure;
    Vector2 startCoords;
    IEnumerator moveBottomCoroutine;
    const float MIN_BOTTOM_TIME = 0.01f;
    const float FAST_BOTTOM_TIME = 0.05f;
    const float BOTTOM_TIME_STEP = 0.01f;
    const float START_BOTTOM_TIME = 0.2f;
    float bottomTime = START_BOTTOM_TIME;

    public FigureActionManager(Vector2 startCoords)
    {
        this.startCoords = startCoords;
        ChangedScoreEvent.Instance.AddListener(score => {
            changeBottomTime();
            });
    }

    public IEnumerator getMoveBottomCoroutine()
    {
        if (moveBottomCoroutine == null)
        {
            moveBottomCoroutine = moveBottomFigure();
        }

        return moveBottomCoroutine;
    }

    protected IEnumerator moveBottomFigure()
    {
        for (;;)
        {
            if (figure is not null)
            {
                figure.moveBottom();   
            }
            
            float time = getBottomTime();

            if (isDownArrowPressed && time > FAST_BOTTOM_TIME)
            {
                time = FAST_BOTTOM_TIME;
            }

            yield return new WaitForSeconds(time);
        }
    }

    protected float getBottomTime()
    {
        return bottomTime;
    }

    protected void changeBottomTime()
    {
        if (bottomTime > MIN_BOTTOM_TIME)
        {
            bottomTime -= BOTTOM_TIME_STEP;
        }
    }

    protected void finishedMove()
    {
        HashSet<int> checkColsToFill = new HashSet<int>();

        foreach (Tile tile in figure)
        {
            TileFields.addTile(tile);
            checkColsToFill.Add(tile.Y);
        }

        HashSet<int> colsUpperMoveToBottom = getColsUpperMoveToBottom(checkColsToFill);

        if (colsUpperMoveToBottom.Count > 0)
        {
            ChangedScoreEvent.Instance.Invoke(colsUpperMoveToBottom.Count);
        }

        // Перемещаем строки вниз
        foreach (int col in colsUpperMoveToBottom)
        {
            TileFields.moveBottomHigherThan(col);
        }

        figure = FigureFabric.instanceFigure(startCoords);

        if (TileFields.hasFigureIntersection(figure))
        {
            FinishedGameEvent.Instance.Invoke();

            FinishedMoveBottomEvent.Instance.RemoveListener(finishedMove);
            figure.delete();
            figure = null;

            bottomTime = START_BOTTOM_TIME;
        }
    }

    protected HashSet<int> getColsUpperMoveToBottom(HashSet<int> checkColsToFill)
    {
        HashSet<int> colsUpperMoveToBottom = new HashSet<int>();
        
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

        return colsUpperMoveToBottom;
    }

    public void instanceFigure()
    {
        figure = FigureFabric.instanceFigure(startCoords);
        FinishedMoveBottomEvent.Instance.AddListener(finishedMove);
    }

    public void update()
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

        isDownArrowPressed = Input.GetKey(KeyCode.DownArrow);
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
}