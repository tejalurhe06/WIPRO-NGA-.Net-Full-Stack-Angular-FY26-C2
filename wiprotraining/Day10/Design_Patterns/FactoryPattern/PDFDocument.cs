using System;

namespace Design_Patterns.FactoryPattern
{
    public class PDFDocument : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Opening PDF Document...");
        }
    }
}