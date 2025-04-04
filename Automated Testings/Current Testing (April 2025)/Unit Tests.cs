using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentGradeCalculator;
using System;

namespace StudentGradeCalculatorTests
{
    [TestClass]
    public class GradeCalculatorTests
    {
        private GradeCalculator calculator;

        [TestInitialize]
        public void TestInitialize()
        {
            calculator = new GradeCalculator();
        }

        [TestMethod]
        public void ValidateAndGetScore_ValidInput_ReturnsCorrectScore()
        {
            int result = calculator.ValidateAndGetScore("15", 22);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateAndGetScore_NegativeInput_ThrowsException()
        {
            calculator.ValidateAndGetScore("-5", 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateAndGetScore_ExceedsMaximum_ThrowsException()
        {
            calculator.ValidateAndGetScore("25", 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateAndGetScore_NonNumericInput_ThrowsException()
        {
            calculator.ValidateAndGetScore("abc", 22);
        }

        [TestMethod]
        public void GetGrade_ExcellentRange_ReturnsExcellent()
        {
            string result = calculator.GetGrade(60);
            Assert.AreEqual("«5» (отлично)", result);
        }

        [TestMethod]
        public void GetGrade_GoodRange_ReturnsGood()
        {
            string result = calculator.GetGrade(40);
            Assert.AreEqual("«4» (хорошо)", result);
        }

        [TestMethod]
        public void GetGrade_SatisfactoryRange_ReturnsSatisfactory()
        {
            string result = calculator.GetGrade(20);
            Assert.AreEqual("«3» (удовлетворительно)", result);
        }

        [TestMethod]
        public void GetGrade_UnsatisfactoryRange_ReturnsUnsatisfactory()
        {
            string result = calculator.GetGrade(10);
            Assert.AreEqual("«2» (неудовлетворительно)", result);
        }

        [TestMethod]
        public void GetGrade_LowerBoundaries_ReturnsCorrectGrade()
        {
            Assert.AreEqual("«5» (отлично)", calculator.GetGrade(56));
            Assert.AreEqual("«4» (хорошо)", calculator.GetGrade(32));
            Assert.AreEqual("«3» (удовлетворительно)", calculator.GetGrade(16));
            Assert.AreEqual("«2» (неудовлетворительно)", calculator.GetGrade(0));
        }

        [TestMethod]
        public void GetGrade_UpperBoundaries_ReturnsCorrectGrade()
        {
            Assert.AreEqual("«5» (отлично)", calculator.GetGrade(80));
            Assert.AreEqual("«4» (хорошо)", calculator.GetGrade(55));
            Assert.AreEqual("«3» (удовлетворительно)", calculator.GetGrade(31));
            Assert.AreEqual("«2» (неудовлетворительно)", calculator.GetGrade(15));
        }

        [TestMethod]
        public void GetGrade_TotalExceedsMaximum_StillCalculates()
        {
            string result = calculator.GetGrade(100);
            Assert.AreEqual("«5» (отлично)", result);
        }
    }

    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void CalculateTotalAndGrade_ValidInputs_ReturnsCorrectResults()
        {
            var calculator = new GradeCalculator();
            
            int dbScore = calculator.ValidateAndGetScore("20", GradeCalculator.MaxDatabaseScore);
            int swScore = calculator.ValidateAndGetScore("30", GradeCalculator.MaxSoftwareScore);
            int maintScore = calculator.ValidateAndGetScore("15", GradeCalculator.MaxMaintenanceScore);
            
            int totalScore = dbScore + swScore + maintScore;
            string grade = calculator.GetGrade(totalScore);
            
            Assert.AreEqual(65, totalScore);
            Assert.AreEqual("«5» (отлично)", grade);
        }

        [TestMethod]
        public void CalculateTotalAndGrade_MinValues_ReturnsCorrectResults()
        {
            var calculator = new GradeCalculator();
            
            int dbScore = calculator.ValidateAndGetScore("0", GradeCalculator.MaxDatabaseScore);
            int swScore = calculator.ValidateAndGetScore("0", GradeCalculator.MaxSoftwareScore);
            int maintScore = calculator.ValidateAndGetScore("0", GradeCalculator.MaxMaintenanceScore);
            
            int totalScore = dbScore + swScore + maintScore;
            string grade = calculator.GetGrade(totalScore);
            
            Assert.AreEqual(0, totalScore);
            Assert.AreEqual("«2» (неудовлетворительно)", grade);
        }

        [TestMethod]
        public void CalculateTotalAndGrade_MaxValues_ReturnsCorrectResults()
        {
            var calculator = new GradeCalculator();
            
            int dbScore = calculator.ValidateAndGetScore("22", GradeCalculator.MaxDatabaseScore);
            int swScore = calculator.ValidateAndGetScore("38", GradeCalculator.MaxSoftwareScore);
            int maintScore = calculator.ValidateAndGetScore("20", GradeCalculator.MaxMaintenanceScore);
            
            int totalScore = dbScore + swScore + maintScore;
            string grade = calculator.GetGrade(totalScore);
            
            Assert.AreEqual(80, totalScore);
            Assert.AreEqual("«5» (отлично)", grade);
        }

        [TestMethod]
        public void CalculateTotalAndGrade_GradeThresholds_ReturnsCorrectResults()
        {
            var calculator = new GradeCalculator();
            
            Assert.AreEqual("«2» (неудовлетворительно)", calculator.GetGrade(15));
            Assert.AreEqual("«3» (удовлетворительно)", calculator.GetGrade(16));
            Assert.AreEqual("«4» (хорошо)", calculator.GetGrade(32));
            Assert.AreEqual("«5» (отлично)", calculator.GetGrade(56));
        }
    }
}
