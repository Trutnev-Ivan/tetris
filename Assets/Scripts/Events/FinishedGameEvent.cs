using UnityEngine.Events;

class FinishedGameEvent: UnityEvent
{
    protected static FinishedGameEvent finishedGame;

    public static FinishedGameEvent Instance
    {
        get
        {
            if(finishedGame == null)
            {
                finishedGame = new FinishedGameEvent();
            }

            return finishedGame;
        }

        private set => finishedGame = value;
    }
}