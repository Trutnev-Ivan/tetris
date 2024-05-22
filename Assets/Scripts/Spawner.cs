using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Start()
    {
        Debug.Log(tetris.Settings.instance.getTest());
    }
}