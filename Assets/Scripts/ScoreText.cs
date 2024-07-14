using TMPro;
using UnityEngine;
using tetris.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    protected int score = 0;

    void Start()
    {
        restart();

        ChangedScoreEvent.Instance.AddListener(drawScore);
        RestartGameEvent.Instance.AddListener(restart);
    }

    protected void drawScore(int score)
    {
        this.score += score;
        GetComponent<TextMeshProUGUI>().text = "Score: " + this.score;
    }

    protected void restart()
    {
        score = 0;
        drawScore(score);
    }
}
