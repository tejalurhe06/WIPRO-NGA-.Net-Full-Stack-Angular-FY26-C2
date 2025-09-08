using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestDemo;

namespace ClassLibrary_MSTest
{
    [TestClass]
    public class CalculatorTest
    {
        Calculator c = new Calculator();

        [TestMethod]
        public void solutionAdd()
        {
            double result = c.Add(2, 40);
            Assert.AreEqual(42, result);
        }
        [TestMethod]
        public void solutionSubtract()
        {
            double result = c.Subtract(40, 5);
            Assert.AreEqual(35, result);
        }
        [TestMethod]
        public void solutionEdgeCaseAddZero()
        {
            double result = c.Add(5, 0);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void solutionEdgeCaseSubtractZero()
        {
            double result = c.Subtract(5, 0);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void solutionEdgeCaseMultiplyZero()
        {
            double result = c.Multiply(5, 0);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void solutionMultiply()
        {
            double result = c.Multiply(2, 3);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void solutionDivide()
        {
            double result = c.Divide(10, 2);
            Assert.AreEqual(5,result);
        }

        [TestMethod]
        public void solutionDivideByZero()
        {
            Assert.Throws<DivideByZeroException>(() => c.Divide(10, 0));
        }
    }
}
