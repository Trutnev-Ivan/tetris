using TMPro;
using UnityEngine;
using tetris.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    public static ChangedScoreEvent changedScoreEvent;
    protected int score = 0;

    void Start()
    {
        if (changedScoreEvent == null){
            changedScoreEvent = new ChangedScoreEvent();
        }

        changedScoreEvent.AddListener(drawScore);
        drawScore(score);
    }

    protected void drawScore(int score)
    {
        this.score += score;
        GetComponent<TextMeshProUGUI>().text = "Score: " + this.score;
    }
}
