using System;

namespace AirDischarge
{
	public class DiffEq
	{
		private readonly ParamFi _paramFi;
		private readonly DichotomyDensity _dichotomyDensity;
        private readonly PneumaticParams _pneumaticParams;
        private readonly IVessel _vesselOne;
		private readonly IVessel _vesselTwo;
		private readonly double[] _densityRange = { 0d, 500d };
		public const double InitialTemperature = 273; //В Кельвинах

		public DiffEq(ParamFi paramFi, DichotomyDensity dichotomyDensity,
            PneumaticParams pneumaticParams, IVessel vesselOne, IVessel vesselTwo)
		{
			_paramFi = paramFi;
			_dichotomyDensity = dichotomyDensity;
            _pneumaticParams = pneumaticParams;
            _vesselOne = vesselOne;
			_vesselTwo = vesselTwo;
		}

		public Func<double, double, double> GetDiffEquation()
        {
            double Eq(double t, double x) => 
                (-_paramFi.CurrentFi(x, _vesselTwo.CurrentPressure) * _pneumaticParams.F / _pneumaticParams.V0 * 
                 Math.Sqrt(_vesselOne.CurrentPressure / _dichotomyDensity.Calculate(_densityRange[0], _densityRange[1], _vesselOne.CurrentPressure, InitialTemperature)) *
                 _vesselOne.CurrentPressure) / (1 / PneumaticParams.PolytropicRate * Math.Pow(x / _vesselOne.CurrentPressure, 1d / 2 * PneumaticParams.PolytropicRate - 3d / 2));

            return Eq;
        }
	}
}
