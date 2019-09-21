using System;
using Autofac;

namespace AirDischarge
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            var startPressure = 4E5;
            var endPressure = 1E5;
            double time = 0;
            while (time < 20)
            {
                Console.WriteLine("PressureHead " + Math.Round(startPressure / 1E5, 4) + " Time    " + Math.Round(time, 2));
                startPressure = program.Calculation(startPressure, endPressure, 0.1);
                time += 0.1;
            }

            Console.ReadKey();
        }

        private double Calculation(double head, double tail, double dt)
        {
            var container = CompositionRoot();
            container.Resolve<ConstantParams>().Calculate();
            var diffEquation = container.Resolve<DiffEq>().GetDiffEquation();
            var vesselOne = container.Resolve<VesselOne>();
            var vesselTwo = container.Resolve<VesselTwo>();
            var rungeKutta = container.Resolve<RungeKutta>();

            vesselOne.CurrentPressure = head;
            vesselTwo.CurrentPressure = tail;

            //var dt = 0.01d; //В секундах

            if ((vesselOne.CurrentPressure - vesselTwo.CurrentPressure) > 0.001E5)
            {
                var rk6 = rungeKutta.Rk6(0, dt, vesselOne.CurrentPressure, diffEquation);
                if (!double.IsNaN(rk6))
                {
                    var deltaP = vesselOne.CurrentPressure - rk6;
                    vesselOne.CurrentPressure -= deltaP;
                }
            }

            return vesselOne.CurrentPressure;
        }

        private IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PneumaticParams>().SingleInstance();
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
