using UnityEditor;
using UnityEngine;

namespace tetris
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Settings", order = 1)]
    public class Settings : ScriptableSingleton<Settings>
    {
        [SerializeField]
        private string test;

        public string getTest()
        {
            return test;
        }
    }   
}