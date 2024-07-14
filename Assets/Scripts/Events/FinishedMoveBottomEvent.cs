using UnityEngine.Events;

namespace tetris.Events{
    class FinishedMoveBottomEvent: UnityEvent
    {
        private static FinishedMoveBottomEvent finishedMoveBottom;

        public static FinishedMoveBottomEvent Instance
        {
            get {
                if (finishedMoveBottom == null){
                    finishedMoveBottom = new FinishedMoveBottomEvent();
                }

                return finishedMoveBottom; 
            }

            private set => finishedMoveBottom = value;
        }
    }
}