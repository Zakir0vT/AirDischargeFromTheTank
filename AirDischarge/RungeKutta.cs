using System;

namespace AirDischarge
{
	public class RungeKutta
	{
		public double Rk6(double t0, double dt, double x0, Func<double, double, double> f)
		{
				var x = x0;
				var t = t0;
				var k1 = f(t, x);
				var k2 = f(t + dt / 2, x + k1 * dt / 2);
				var k3 = f(t + dt / 2, x + k2 * dt / 2);
				var k4 = f(t + dt / 2, x + k3 * dt / 2);
				var k5 = f(t + dt / 2, x + k4 * dt / 2);
				var k6 = f(t + dt, x + k5 * dt);

				x += dt / 10 * (k1 + 2 * k2 + 2 * k3 + 2 * k4 + 2 * k5 + k6);
				x = Math.Round(x, 4);

				return x;
		}
	}
}
