namespace AirDischarge
{
    public class PneumaticParams
    {
        //Параметры газа
        public const double PolytropicRate = 1.4d;
        public const double UniversalGasConstant = 8314.472d;
        public readonly double[] MolarFraction = { 0.999980d, 0.000005d, 0.000007d, 0.000002d, 0.000003d };
        public readonly double[] MolarMass = { 28d, 32d, 18d, 2d, 16d };
        public readonly double[] CriticalPressureComponents = { 3.39E6d, 5.04E6d, 22.13E6d, 1.3E6d, 4.226E6d };
        public readonly double[] TemperatureCriticalComponents = { 126.25d, 154.5d, 374d, 32.8d, 191d };
        public readonly int NumberOfComponents = 5;

		//Параметры резервуара
        public const double F = 1.577E-4d; // В м2
        public const double V0 = 0.5d; //В м3
    }
}
