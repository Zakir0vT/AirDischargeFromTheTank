using Newtonsoft.Json;

namespace AirDischarge
{
    public class PneumaticParams : PneumaticParamsConf
    {
        //Gas parameters
        [JsonProperty("PolytropicRate")]
        public const double PolytropicRate = 1.4d;

        [JsonProperty("UniversalGasConstant")]
        public const double UniversalGasConstant = 8314.472d;

        [JsonProperty("MolarFraction")]
        public readonly double[] MolarFraction = { 0.999980d, 0.000005d, 0.000007d, 0.000002d, 0.000003d };

        [JsonProperty("MolarMass")]
        public readonly double[] MolarMass = { 28d, 32d, 18d, 2d, 16d };

        [JsonProperty("CriticalPressureComponents")]
        public readonly double[] CriticalPressureComponents = { 3.39E6d, 5.04E6d, 22.13E6d, 1.3E6d, 4.226E6d };

        [JsonProperty("TemperatureCriticalComponents")]
        public readonly double[] TemperatureCriticalComponents = { 126.25d, 154.5d, 374d, 32.8d, 191d };

        [JsonProperty("NumberOfComponents")]
        public readonly int NumberOfComponents = 5;

        //Tank parameters
        [JsonProperty("Hole cross-sectional area")]
        public readonly double F = 1.577E-4d; // square meters

        [JsonProperty("Tank volume")]
        public readonly double V0 = 0.5d; // cubic meters
    }
}
