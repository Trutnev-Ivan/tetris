namespace tetris.Figures.Enum
{
    public enum RotateState
    {
        BOTTOM,
        LEFT,
        TOP,
        RIGHT
    }
    
    public static class RotateStateMethods
    {
        public static RotateState next(this RotateState rotateState)
        {
            switch (rotateState)
            {
                case RotateState.BOTTOM:
                    return RotateState.LEFT;
                case RotateState.LEFT:
                    return RotateState.TOP;
                case RotateState.TOP:
                    return RotateState.RIGHT;
                case RotateState.RIGHT:
                    return RotateState.BOTTOM;
            }
    
            return RotateState.BOTTOM;
        }
    }
}