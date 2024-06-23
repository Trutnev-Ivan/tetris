using System;
using UnityEngine;

namespace tetris.Figures
{
    public class FigureFabric
    {
        protected static int i = -1;
        protected static Type[] figureClasses =
        {
            typeof(JFigure),
            typeof(LFigure),
            typeof(Line),
            typeof(SFigure),
            typeof(Square),
            typeof(TFigure),
            typeof(ZFigure)
        };
        
        public static Figure instanceFigure(Vector2 startCoords)
        {
            ++i;
            i %= figureClasses.Length;
            
            return (Figure)Activator.CreateInstance(figureClasses[i], startCoords);
        }
    }
}