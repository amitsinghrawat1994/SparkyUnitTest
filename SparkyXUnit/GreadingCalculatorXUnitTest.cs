using Sparky;
using Xunit;

namespace SparkyXUnit
{
    public class GreadingCalculatorXUnitTest
    {
        private GreadingCalculator gradingCalculator;

        public GreadingCalculatorXUnitTest()
        {
            gradingCalculator = new GreadingCalculator();
        }

        [Fact]
        public void GradeCalc_InputScore95Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            string result = gradingCalculator.GetGrade();
            Assert.Equal("A", result);
        }

        [Fact]
        public void GradeCalc_InputScore85Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            string result = gradingCalculator.GetGrade();
            Assert.Equal("B", result);
        }

        [Fact]
        public void GradeCalc_InputScore65Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            string result = gradingCalculator.GetGrade();
            Assert.Equal("C", result);
        }

        [Fact]
        public void GradeCalc_InputScore95Attendance65_GetAGrade()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            string result = gradingCalculator.GetGrade();
            Assert.Equal("B", result);
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GradeCalc_FailreScnairos_GetFGrade(int score, int attendance)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            string result = gradingCalculator.GetGrade();
            Assert.Equal("F", result);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
                            
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GradeCalc_AllGradeLogicalScenarios_GradeOutput(int score, int attendance, string expectedResult)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            var result= gradingCalculator.GetGrade();
            Assert.Equal(expectedResult, result);
        }
    }
}
