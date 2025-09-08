using System; //package import

namespace Demos
{
    class Demos
    {
        static void Main(string[] args)
        {
            //printing on Console
            Console.Write("HelloWorld");
            Console.WriteLine("My first C# code");

            //datatypes
            //variable is a container that contains value
            char ch = 'B';

            string s = "Tejal";

            short s = 143;  //==Int16

            int i = 456;    //==Int32

            long l = 9816438; //==Int64

            double d = 56.7;

            decimal dd = 56.3M; //use M as suffix to differentiate double

            int* ptr = &ch;    //pointer data type use to store address of other variable

            //when we dont know the datatype of variable
            var i = 10;
            var name = "Tej";
            Console.WriteLine("Size of var dataype : " + sizeof(name));


            Console.WriteLine("Character : " + ch);
            Console.WriteLine("Eqivalent number : " + (byte)ch);  //conversion implicit and explicit
            Console.WriteLine("The minimum and maximum value : " + (int)char.MinValue + " " + (int)char.MaxValue);  //properties of variable
            Console.WriteLine("Size :" + sizeof(char));

            //template literal can also be used for printing
            Console.WriteLine($"The value of i is {i}");

            //take input from user
            Console.WriteLine("Enter first number");
            int a = Convert.ToInt32(Console.ReadLine());   //incoming string input converted into int value
            Console.WriteLine("Enter second number");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Result : "+ (a+b));
            Console.ReadKey();
        }
        
    }
};


