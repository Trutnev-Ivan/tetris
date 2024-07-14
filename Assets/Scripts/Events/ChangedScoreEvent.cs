using UnityEngine.Events;

namespace tetris.Events
{
    public class ChangedScoreEvent: UnityEvent<int>
    {
        private static ChangedScoreEvent changedScoreEvent;

        public static ChangedScoreEvent Instance
        {
            get {
                if (changedScoreEvent == null){
                    changedScoreEvent = new ChangedScoreEvent();
                }

                return changedScoreEvent; 
            }

            private set => changedScoreEvent = value;
        }
    }
}