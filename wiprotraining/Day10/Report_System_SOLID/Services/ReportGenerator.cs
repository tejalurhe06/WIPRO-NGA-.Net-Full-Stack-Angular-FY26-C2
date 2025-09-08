using Report_System.Interfaces;

namespace Report_System.Services
{
    // Responsible for generating report content (SRP)
    public class ReportGenerator
    {
        private readonly IReportContent _report;

        public ReportGenerator(IReportContent report)
        {
            _report = report;
        }

        public string Generate()
        {
            return _report.GetContent();
        }
    }
}