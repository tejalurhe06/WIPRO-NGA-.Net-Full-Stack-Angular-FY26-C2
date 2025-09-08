using NUnit.Framework;
using NUnitTestingDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Test
{
    [TestFixture]
    public class CalculatorTest
    {
        Calculator c = new Calculator();

        [Test]
        public void solutionAdd()
        {
            double result = c.Add(2, 40);
            Assert.That(result, Is.EqualTo(42.0));
        }
        [Test]
        public void solutionSubtract()
        {
            double result = c.Subtract(40,5);
            Assert.That(result, Is.EqualTo(35));
        }
        [Test] 
        public void solutionEdgeCaseAddZero()
        {
            double result = c.Add(5, 0);
            Assert.That(result,Is.EqualTo(5));
        }

        [Test]
        public void solutionEdgeCaseSubtractZero()
        {
            double result = c.Subtract(5, 0);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void solutionEdgeCaseMultiplyZero()
        {
            double result = c.Multiply(5, 0);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void solutionMultiply()
        {
            double result = c.Multiply(2, 3);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void solutionDivide()
        {
            double result = c.Divide(10, 2);
            Assert.That(result, Is.EqualTo(5.0));
        }

        [Test]
        public void solutionDivideByZero()
        {
            Assert.Throws<DivideByZeroException>(() => c.Divide(10, 0));
        }
    }
}
