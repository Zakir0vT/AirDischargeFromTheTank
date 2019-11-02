using AirDischarge;
using Xunit;

namespace AirDischargeTests
{
    public class ConstantParamsTest
    {
        [Fact]
        public void ConstantParams()
        {
            var constParams = new ConstantParams(new PneumaticParams());
            constParams.Calculate();

            Assert.Equal(0.0199d, constParams.A);
            Assert.Equal(0d, constParams.B);
            Assert.Equal(0.5283d, constParams.Xcr);
        }
    }
}
