using System;

namespace ISP
{


    //Without ISP
    // public interface IWorker
    // {
    //     void Work();
    //     void Eat();
    // }

    // public class Robot : IWorker
    // {
    //     public void Work() =>
    //     Console.WriteLine("Robot Working");

    //     public void Eat() =>
    //     throw new NotImplementedException();
    // }

    //With ISP

    public interface IWorkable
    {
        void Work();
    }

    public interface IEatable
    {
        void Eat();
    }

    public class Human : IWorkable, IEatable
    {
        public void Work() =>
        Console.WriteLine("Human Working");

        public void Eat() =>
        Console.WriteLine("Human Eating");
    }

    public class Robot : IWorkable
    {
        public void Work() =>
        Console.WriteLine("Robot Working");
    }
}