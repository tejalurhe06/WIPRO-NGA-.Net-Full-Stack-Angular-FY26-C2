using StructuralPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralPattern
{
    // Adaptee: Legacy class with incompatible interface
    public class LegacyPrinter
    {
        public void PrintDocument()
        {
            Console.WriteLine("Printing document using LegacyPrinter...");
        }
    }

    // Target Interface: What the client expects
    public interface IPrinter
    {
        void Print();
    }

    // Adapter: Converts LegacyPrinter to IPrinter
    public class PrinterAdapter : IPrinter
    {
        private readonly LegacyPrinter _legacyPrinter;

        public PrinterAdapter(LegacyPrinter legacyPrinter)
        {
            _legacyPrinter = legacyPrinter;
        }

        public void Print()
        {
            _legacyPrinter.PrintDocument(); // Translate call
        }
    }
}

// Client Code
class Program
{
    static void Main()
    {
        IPrinter printer = new PrinterAdapter(new LegacyPrinter());
        printer.Print(); // Works like a charm!
    }
}
