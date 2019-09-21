using System;

namespace AirDischarge
{
    public class ParamX
    {
        private double _currentX;

        public double CurrentX(double currentP, double pressureInTail)
        {
            _currentX = pressureInTail / currentP;

            return Math.Round(_currentX, 4);
        }
    }
}