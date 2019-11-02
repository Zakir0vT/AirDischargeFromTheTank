using AirDischarge;
using Xunit;

namespace AirDischargeTests
{
    public class ParamXTest
    {
        [Fact]
        public void GetCurrentParamXTest()
        {
            var paramX = new ParamX();
            var currentP = 25d;
            var pressureInTail = 100d;

            Assert.Equal(4, paramX.CurrentX(currentP, pressureInTail));
        }

        [Theory]
        [InlineData(100d, 25d)]
        [InlineData(25d, 100d)]
        [InlineData(33d, 99d)]
        public void GetCurrentParamXTheory(double currentP, double pressureInTail)
        {
            var paramX = new ParamX();
            var calculatedValue = paramX.CurrentX(currentP, pressureInTail);

            Assert.True(calculatedValue.GetType() == typeof(double));
        }
    }
}
