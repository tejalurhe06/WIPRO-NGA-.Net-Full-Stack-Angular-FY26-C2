using BehaviouralPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviouralPattern
{
    // Handler interface
    public abstract class Approver
    {
        protected Approver _next;

        public void SetNext(Approver next) => _next = next;

        public abstract void ProcessRequest(PurchaseRequest request);
    }
    public class Manager : Approver
    {
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 10000)
                Console.WriteLine("Manager approved request for ₹" + request.Amount);
            else
                _next?.ProcessRequest(request);
        }
    }

    public class Director : Approver
    {
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 50000)
                Console.WriteLine("Director approved request for ₹" + request.Amount);
            else
                _next?.ProcessRequest(request);
        }
    }
    public class VicePresident : Approver
    {
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 100000)
                Console.WriteLine("Vice President approved request for ₹" + request.Amount);
            else
                Console.WriteLine("Request for ₹" + request.Amount + " requires board approval.");
        }
    }

    public class PurchaseRequest
    {
        public double Amount { get; set; }

        public PurchaseRequest(double amount)
        {
            Amount = amount;
        }
    }
}


class Program
{
    static void Main()
    {
        Approver manager = new Manager();
        Approver director = new Director();
        Approver vp = new VicePresident();

        manager.SetNext(director);
        director.SetNext(vp);

        var requests = new[]
        {
            new PurchaseRequest(5000),
            new PurchaseRequest(30000),
            new PurchaseRequest(75000),
            new PurchaseRequest(150000)
        };

        foreach (var request in requests)
        {
            manager.ProcessRequest(request);
        }
    }
}
