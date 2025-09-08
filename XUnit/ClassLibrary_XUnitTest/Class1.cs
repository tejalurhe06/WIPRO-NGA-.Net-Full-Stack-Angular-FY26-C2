using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnit;

namespace ClassLibrary_XUnitTest
{
    public class CalculatorTest
    {
        Calculator c = new Calculator();

        [Fact]
        public void solutionAdd()
        {
            double result = c.Add(2, 40);
            Assert.Equal(42, result);
        }
        [Fact]
        public void solutionSubtract()
        {
            double result = c.Subtract(40, 5);
            Assert.Equal(35, result);
        }
        
        [Fact]
        public void solutionEdgeCaseAddZero()
        {
            double result = c.Add(5, 0);
            Assert.Equal(5, result);
        }

        [Fact]
        public void solutionEdgeCaseSubtractZero()
        {
            double result = c.Subtract(5, 0);
            Assert.Equal(5, result);
        }

        [Fact]
      
        public void solutionEdgeCaseMultiplyZero()
        {
            double result = c.Multiply(5, 0);
            Assert.Equal(0, result);
        }

        [Fact]
        
        public void solutionMultiply()
        {
            double result = c.Multiply(2, 3);
            Assert.Equal(6, result);
        }

        [Fact]
        public void solutionDivide()
        {
            double result = c.Divide(10, 2);
            Assert.Equal(5, result);
        }

        [Fact]
        public void solutionDivideByZero()
        {
            Assert.Throws<DivideByZeroException>(() => c.Divide(10, 0));
        }
    }
}
