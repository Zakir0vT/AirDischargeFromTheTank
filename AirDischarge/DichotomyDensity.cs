using System;

namespace AirDischarge
{
    public class DichotomyDensity
    {
        private readonly ConstantParams _constantParams;

        public DichotomyDensity(ConstantParams constantParams)
        {
            _constantParams = constantParams;
        }

        //Нахождение плотности методом дихотомии
        public double Calculate(double ro1, double ro2, double p, double T)
        {
		        const double eps = 0.000001d;
		        double ro3;
		        do
		        {
			        ro3 = (ro1 + ro2) / 2;
			        if (Density(ro1, p, T) * Density(ro3, p, T) < 0)
				        ro2 = ro3;
			        else
				        ro1 = ro3;
		        } while (Math.Abs(Density(ro3, p, T)) >= eps);

		        return Math.Round(ro3, 4);
        }

        private double Density(double ro, double p, double T)
        {
		        ro = 1 / ro;
		        var density = (_constantParams.GasConstantOfTheMixture * T / (ro - _constantParams.B)) -
		                    (_constantParams.A / (ro * (ro + _constantParams.B) * Math.Pow(T, 0.5))) - p;

				return density;
        }
    }
}
