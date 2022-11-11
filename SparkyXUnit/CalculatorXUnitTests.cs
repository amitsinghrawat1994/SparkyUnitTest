using Sparky;
using Xunit;

namespace SparkyXUnit
{
    public class CalculatorXUnitTests
    {
        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calc = new();

            //Act
            int result = calc.AddNumbers(10, 20);

            //Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void IsOddChecker_InputEvenNumbear_ReturnFalse()
        {
            //arrange
            Calculator calc = new();

            // act
            bool isOdd = calc.IsOddNumber(10);

            //asert
            //Assert.That(isOdd, Is.EqualTo(false));
            Assert.False(isOdd);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(13)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            //arrange
            Calculator calc = new();

            // act
            bool isOdd = calc.IsOddNumber(a);

            //asert
            Assert.True(isOdd);
        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int a, bool exptectedResult)
        {
            Calculator calc = new();
            var result = calc.IsOddNumber(a);
            Assert.Equal(exptectedResult, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)]
        //[InlineData(5.43, 10.53)]
        //[InlineData(5.49, 10.59)]
        public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange
            Calculator calc = new();

            //Act
            double result = calc.AddNumbersDoubles(a, b);

            //Assert
            Assert.Equal(15.9, result, 1);
        }

        [Fact]
        public void OddRanger_InputMinAndMaxRange_ReturnValidOddNumberRange()
        {
            Calculator calc = new();
            List<int> exptectedOddRange = new() { 5, 7, 9 };  //5-10

            // Act
            List<int> result = calc.GetOddRange(5, 10);

            // Assert
            Assert.Equal(exptectedOddRange, result);
            //Assert.AreEqual(exptectedOddRange, result);
            //Assert.Contains(7, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3 ,result.Count);
            Assert.DoesNotContain(6, result);
            Assert.Equal(result.OrderBy(u => u), result);
            //Assert.That(result, Is.Unique);
        }
    }
}
