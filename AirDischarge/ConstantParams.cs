using System;

namespace AirDischarge
{
    public class ConstantParams
    {
        private readonly PneumaticParams _pneumaticParams;
        private double _molarMassOfTheMixture;
        private double _criticalPressure;
        private double _criticalTemperature;
        public double GasConstantOfTheMixture { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Xcr { get; set; }

        public ConstantParams(PneumaticParams pneumaticParams)
        {
            _pneumaticParams = pneumaticParams;
        }

        public void Calculate()
        {
            //Молярная масса смеси
            for (int i = 0; i < _pneumaticParams.NumberOfComponents; i++)
            {
                _molarMassOfTheMixture += _pneumaticParams.MolarFraction[i] * _pneumaticParams.MolarMass[i];
            }
            _molarMassOfTheMixture = Math.Round(_molarMassOfTheMixture, 4);

            //Газовая постоянная смеси
            GasConstantOfTheMixture = PneumaticParams.UniversalGasConstant / _molarMassOfTheMixture;
            GasConstantOfTheMixture = Math.Round(GasConstantOfTheMixture, 4);

            //Давление и температура критические
            for (int i = 0; i < _pneumaticParams.NumberOfComponents; i++)
            {
                _criticalPressure += _pneumaticParams.MolarFraction[i] * _pneumaticParams.CriticalPressureComponents[i];
                _criticalTemperature += _pneumaticParams.MolarFraction[i] * _pneumaticParams.TemperatureCriticalComponents[i];
            }
            _criticalPressure = Math.Round(_criticalPressure, 4);
            _criticalTemperature = Math.Round(_criticalTemperature, 4);

            //Параметры a и b для уравнения Редлиха-Квонга
            A = 0.42748f * (Math.Pow(GasConstantOfTheMixture, 2) * Math.Pow(_criticalTemperature, 2.5f)) / (_criticalPressure * 100000);
            A = Math.Round(A, 4);
            B = 0.08664f * GasConstantOfTheMixture * _criticalTemperature / (_criticalPressure * 100000);
            B = Math.Round(B, 4);

            Xcr = Math.Pow((2 / (PneumaticParams.PolytropicRate + 1)), (PneumaticParams.PolytropicRate / (PneumaticParams.PolytropicRate - 1)));
            Xcr = Math.Round(Xcr, 4);
        }
    }
}
