using Report_System.Interfaces;

namespace Report_System.Formatters
{
    // Formatter that returns report in Excel format (simulated)
    public class ExcelFormatter : IReportFormatter
    {
        public string Format(string content)
        {
            return $"[Excel]: {content}";
        }
    }
}