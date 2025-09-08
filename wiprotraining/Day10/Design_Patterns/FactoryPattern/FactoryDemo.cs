using System;

namespace Design_Patterns.FactoryPattern
{
    public class FactoryDemo
    {
        public static void Run()
        {
            Console.WriteLine("Factory Pattern Demo:");
            IDocument doc1 = DocumentFactory.CreateDocument("pdf");
            doc1?.Open();

            IDocument doc2 = DocumentFactory.CreateDocument("word");
            doc2?.Open();

            Console.WriteLine();
        }
    }
}