using System;

namespace AirDischarge
{
    public class ParamFi
    {
        private readonly ParamX _paramX;
        private readonly ConstantParams _constantParams;

        public ParamFi(ParamX paramX, ConstantParams constantParams)
        {
            _paramX = paramX;
            _constantParams = constantParams;
        }

        //Определение Fi(pt) - скорость течения газа
        public double CurrentFi(double p, double p0)
        {
            var x = _paramX.CurrentX(p, p0);
            double currentFi;

            if (x >= 0 && x <= _constantParams.Xcr)
            {
                currentFi = Math.Pow(2 / (PneumaticParams.PolytropicRate + 1), 1 / (PneumaticParams.PolytropicRate - 1)) *
                            Math.Sqrt(2 * PneumaticParams.PolytropicRate / (PneumaticParams.PolytropicRate + 1));
            }
            else
            {
                currentFi = Math.Sqrt(2 * PneumaticParams.PolytropicRate / (PneumaticParams.PolytropicRate - 1) *
                                      (Math.Pow(x, 2 / PneumaticParams.PolytropicRate) -
                                       Math.Pow(x, (PneumaticParams.PolytropicRate + 1) / PneumaticParams.PolytropicRate)));
            }

            return Math.Round(currentFi, 4);
        }
    }
}
