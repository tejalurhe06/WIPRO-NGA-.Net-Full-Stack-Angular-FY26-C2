using System;

namespace LSP
{


    //Without LSP
    // public class Bird
    // {
    //     public virtual void Fly() =>
    //     Console.WriteLine("Flying");
    // }

    // public class Ostrich : Bird
    // {
    //     public override void Fly()
    //     {
    //         throw new NotImplementedException("Ostrich cant fly");
    //     }
    // }

    //With LSP

    public abstract class Bird { }

    public interface IFlyingBird
    {
        void Fly();
    }

    public class Sparrow : Bird, IFlyingBird
    {
        public void Fly() =>
        Console.WriteLine("Flying");
    }

    public class Ostrich : Bird
    {
        //Does not implement Fly
    }

}