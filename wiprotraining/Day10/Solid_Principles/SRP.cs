using System;

namespace SRP
{


    //Without SRP
    // public class Invoice
    // {
    //     public void CalculateTotal()
    //     {
    //         Console.WriteLine("Calculating total");
    //     }

    //     public void SaveToDatabase()
    //     {
    //         Console.WriteLine("Saving To DB");
    //     }

    //     public void PrintInvoice()
    //     {
    //         Console.WriteLine("Printing Invoice");
    //     }

    // }

    //WITH SRP

    public class Invoice
    {
        public void CalculateTotal()
        {
            Console.WriteLine("Calculating total");
        }
    }

    public class InvoiceRepository
    {
        public void SaveToDatabase()
        {
            Console.WriteLine("Saving To DB");
        }

    }

    public class InvoicePrinter
    {
        public void PrintInvoice()
        {
            Console.WriteLine("Printing Invoice");
        }

    }
}