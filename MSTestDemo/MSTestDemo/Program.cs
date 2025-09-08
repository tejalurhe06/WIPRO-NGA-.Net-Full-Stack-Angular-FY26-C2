using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestDemo
{
    public interface ICalculator
    {
        double Add(double a, double b);
        double Subtract(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
    }
    public class Calculator : ICalculator
    {
        public double Add(double a, double b) { return a + b; }

        public double Subtract(double a, double b) { return a - b; }

        public double Multiply(double a, double b) { return a * b; }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                return a / b;
            }
        }

        static void Main(string[] args)
        {

        }
    }

}
