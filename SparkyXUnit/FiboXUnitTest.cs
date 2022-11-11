using Sparky;
using Xunit;

namespace SparkyXUnit
{

    public class FiboXUnitTest
    {
        [Fact]
        public void FiboChecker_Input1_ReturnFiboSeries()
        {
            List<int> exptectedRange = new List<int> { 0 };

            Fibo fibo = new();
            fibo.Range = 1;

            List<int> result = fibo.GetFiboSeries();

            Assert.NotEmpty(result);
            Assert.Equal(exptectedRange.OrderBy(x => x), result);
            Assert.True(result.SequenceEqual(exptectedRange));
        }

        [Fact]
        public void FiboChecker_Input6_ReturnFiboSeries()
        {
            List<int> exptectedRange = new List<int> { 0, 1, 1, 2, 3, 5 };

            Fibo fibo = new();
            fibo.Range = 6;

            List<int> result = fibo.GetFiboSeries();

            Assert.Contains(3, result);
            Assert.Equal(6, result.Count);
            Assert.DoesNotContain(4, result);
            Assert.Equal(result, exptectedRange);
        }

    }
}
