using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradeCalculatorForEachModule;
using System;

namespace Testings
{
    [TestClass]
    public class CalculatorTests
    {
        private ExactCalculator calculator;

        [TestInitialize]
        public void TestInitialize()
        {
            calculator = new ExactCalculator();
        }

        [TestMethod]
        public void ValidateGathering_ValidInput()
        {
            int result = calculator.ValidateGathering("15", 22);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateGathering_NegativeInput()
        {
            calculator.ValidateGathering("-5", 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateGathering_ExceedsMaximum()
        {
            calculator.ValidateGathering("25", 22);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateGathering_NonNumericInput()
        {
            calculator.ValidateGathering("abc", 22);
        }

        [TestMethod]
        public void Compute_ExcellentmarkIter()
        {
            string result = calculator.Compute(60);
            Assert.AreEqual("5 -> Отлично", result);
        }

        [TestMethod]
        public void Compute_GoodMarkIter_ReturnsGood()
        {
            string result = calculator.Compute(40);
            Assert.AreEqual("4 -> Хорошо", result);
        }

        [TestMethod]
        public void Compute_NotBadIter()
        {
            string result = calculator.Compute(20);
            Assert.AreEqual("3 -> Удовлетворительно", result);
        }

        [TestMethod]
        public void Compute_BadIter()
        {
            string result = calculator.Compute(10);
            Assert.AreEqual("2 -> Неудовлетворительно", result);
        }

        [TestMethod]
        public void Compute_IfCorrectLowers()
        {
            Assert.AreEqual("5 -> Отлично", calculator.Compute(56));
            Assert.AreEqual("4 -> Хорошо", calculator.Compute(32));
            Assert.AreEqual("3 -> Удовлетворительно", calculator.Compute(16));
            Assert.AreEqual("2 -> Неудовлетворительно", calculator.Compute(0));
        }

        [TestMethod]
        public void Compute_IfCorrectHighers()
        {
            Assert.AreEqual("5 -> Отлично", calculator.Compute(80));
            Assert.AreEqual("4 -> Хорошо", calculator.Compute(55));
            Assert.AreEqual("3 -> Удовлетворительно", calculator.Compute(31));
            Assert.AreEqual("2 -> Неудовлетворительно", calculator.Compute(15));
        }
    }
}