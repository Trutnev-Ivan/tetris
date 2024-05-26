using UnityEngine;
using tetris;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    void Start()
    {
        GetComponent<Camera>().orthographicSize = Settings.instance.getHeight() / 2;
    }
}
