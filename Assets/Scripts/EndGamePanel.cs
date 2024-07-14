using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(Settings.instance.getWidth() / 2, Settings.instance.getHeight() / 4, 0);

        hidePanel();

        FinishedGameEvent.Instance.AddListener(showPanel);
        RestartGameEvent.Instance.AddListener(hidePanel);
    }

    protected void showPanel()
    {
        gameObject.SetActive(true);
    }

    protected void hidePanel()
    {
        gameObject.SetActive(false);
    }
}
