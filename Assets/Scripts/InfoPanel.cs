using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Settings.instance.getWidth() + Settings.instance.getOffset() * 5, 0, 100);
        transform.localScale = new Vector3(Settings.instance.getWidth() / 2, Settings.instance.getHeight() / 2, 0);
    }
}