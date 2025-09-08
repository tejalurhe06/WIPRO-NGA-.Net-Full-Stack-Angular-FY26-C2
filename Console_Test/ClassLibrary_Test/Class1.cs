using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Test;
namespace ClassLibrary_Test
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]

        public void solutionAdd()
        {
            Calculator c = new Calculator();
            int result = c.Add(2, 32);

            Assert.That(result, Is.EqualTo(34));
        }

        [Test]

        public void solutionMultiply()
        {
            var c = new Calculator();
            int result = c.Add(2, 3);

            Assert.That(result, Is.EqualTo(6));
        }
    }
}
