using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
        int Multiply(int a, int b);
        double Division(int a, int b);
    }
    class Program : ICalculator
    {

        public int Add(int a, int b)
        {
            int add = a + b;
            return add;
        }

        public int Subtract(int a, int b)
        {
            int subtract = a + b;
            return subtract;
        }

        public int Multiply(int a, int b)
        {
            int multiply = a + b;
            return multiply;
        }

        public double Division(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                int div = a / b;
                return div;
            }
        }
        static void Main(string[] args)
        {
            
            //Console.WriteLine("Enter Number 1 : ");
            //int a = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Enter Number 2 : ");
            //int b = Convert.ToInt32(Console.ReadLine());

            //Program p = new Program();
            //p.Add(a, b);
            //p.Subtract(a, b);
            //p.Multiply(a, b);
            //p.Division(a, b);
        }


    }
}
