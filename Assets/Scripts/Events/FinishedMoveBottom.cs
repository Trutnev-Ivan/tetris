using UnityEngine.Events;

namespace tetris.Events{
    class FinishedMoveBottom: UnityEvent
    {
        private static FinishedMoveBottom finishedMoveBottom;

        public static FinishedMoveBottom Instance
        {
            get {
                if (finishedMoveBottom == null){
                    finishedMoveBottom = new FinishedMoveBottom();
                }

                return finishedMoveBottom; 
            }

            private set => finishedMoveBottom = value;
        }
    }
}