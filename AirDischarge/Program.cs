using System;
using Autofac;

namespace AirDischarge
{
    class Program
    {
        static void Main(string[] args)
        {
            var startPressure = 4E5;
            var endPressure = 1E5;
            var dt = 0.1d; //in seconds
            new Program().Calculation(startPressure, endPressure, dt);

            Console.ReadKey();
        }

        private void Calculation(double head, double tail, double dt)
        {
            var container = CompositionRoot();
            container.Resolve<ConstantParams>().Calculate();
            var diffEquation = container.Resolve<DiffEq>().GetDiffEquation();
            var vesselOne = container.Resolve<VesselOne>();
            var vesselTwo = container.Resolve<VesselTwo>();
            var rungeKutta = container.Resolve<RungeKutta>();

            vesselOne.CurrentPressure = head;
            vesselTwo.CurrentPressure = tail;

            var time = 0d;

            while (vesselOne.CurrentPressure - vesselTwo.CurrentPressure > 0.001E5)
            {
                Console.WriteLine("PressureHead " + Math.Round(vesselOne.CurrentPressure / 1E5, 4) + " Time    " + Math.Round(time, 2));

                var rk6 = rungeKutta.Rk6(0, dt, vesselOne.CurrentPressure, diffEquation);
                if (!double.IsNaN(rk6))
                {
                    var deltaP = vesselOne.CurrentPressure - rk6;
                    vesselOne.CurrentPressure -= deltaP;
                    time += dt;
                }
            }
        }

        private IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(PneumaticParams.GetFromConfig).SingleInstance();
            builder.RegisterType<ConstantParams>().SingleInstance();
            builder.RegisterType<ParamFi>().SingleInstance();
            builder.RegisterType<DichotomyDensity>().SingleInstance();
            builder.RegisterType<ParamX>().SingleInstance();
            builder.RegisterType<ConstantParams>().SingleInstance();
            builder.RegisterType<DiffEq>().SingleInstance();
            builder.RegisterType<RungeKutta>().SingleInstance();

            builder.RegisterType<VesselOne>().SingleInstance().AsSelf().As<IVessel>();
            builder.RegisterType<VesselTwo>().SingleInstance().AsSelf().As<IVessel>();

            return builder.Build();
        }
    }
}
