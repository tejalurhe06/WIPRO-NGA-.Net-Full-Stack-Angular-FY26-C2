using Report_System.Interfaces;

namespace Report_System.Services
{
    // High-level module depending on abstractions (DIP)
    public class ReportService
    {
        private readonly IReportFormatter _formatter;
        private readonly IReportSaver _saver;
        private readonly ReportGenerator _generator;

        public ReportService(IReportFormatter formatter, IReportSaver saver, ReportGenerator generator)
        {
            _formatter = formatter;
            _saver = saver;
            _generator = generator;
        }

        public void ProcessReport(string filePath)
        {
            var content = _generator.Generate();
            var formatted = _formatter.Format(content);
            _saver.Save(formatted, filePath);
        }
    }
}