using UnityEngine.Events;

class RestartGameEvent: UnityEvent
{
    protected static RestartGameEvent restartGame;

    public static RestartGameEvent Instance
    {
        get
        {
            if (restartGame == null)
            {
                restartGame = new RestartGameEvent();
            }

            return restartGame;
        }

        private set => restartGame = value;
    }
}