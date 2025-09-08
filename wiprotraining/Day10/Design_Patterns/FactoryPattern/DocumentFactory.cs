namespace Design_Patterns.FactoryPattern
{
    public static class DocumentFactory
    {
        public static IDocument CreateDocument(string type)
        {
            switch (type.ToLower())
            {
                case "pdf":
                    return new PDFDocument();
                case "word":
                    return new WordDocument();
                default:
                    return null;
            }
        }
    }
}