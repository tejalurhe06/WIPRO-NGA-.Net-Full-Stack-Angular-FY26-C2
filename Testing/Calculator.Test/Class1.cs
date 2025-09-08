using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing;
namespace Calculator.Test
{
    [TestFixture]
      public class Class1
    {
        [Test]
        public void shouldbeaddition()
        {
            var c = new Testing();
            int result = c.Add();
            

        }
    }
}
