using System;
class CalculatorOperations
{
    static void Main(string[] args)
    {
        //Took input
        Console.WriteLine("Enter first number");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter second number");
        int b = Convert.ToInt32(Console.ReadLine());

       //perform operations
        Console.WriteLine("Addition : "+ (a+b));
        Console.WriteLine("Substraction : "+ (a-b));
        Console.WriteLine("Multiplication : "+ (a*b));
        Console.WriteLine("Division : "+ ((float)a/(float)b));

        Console.ReadKey();
        }

}