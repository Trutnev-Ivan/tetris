using TMPro;
using UnityEngine;
using tetris.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    protected int score = 0;

    void Start()
    {
        ChangedScoreEvent.Instance.AddListener(drawScore);
        drawScore(score);
    }

    protected void drawScore(int score)
    {
        this.score += score;
        GetComponent<TextMeshProUGUI>().text = "Score: " + this.score;
    }
}
