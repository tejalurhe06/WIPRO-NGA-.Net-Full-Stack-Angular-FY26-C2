using System;

namespace Design_Patterns.FactoryPattern
{
    public class WordDocument : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Opening Word Document...");
        }
    }
}