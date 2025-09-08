using Report_System.Interfaces;

namespace Report_System.Formatters
{
    // Formatter that returns report in PDF format (simulated)
    public class PdfFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"[PDF]: {content}";
        }
    }
}