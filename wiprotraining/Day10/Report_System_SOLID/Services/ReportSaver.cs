using System.IO;
using Report_System.Interfaces;

namespace Report_System.Services
{
    // Responsible for saving report to file (SRP)
    public class ReportSaver : IReportSaver
    {
        public void Save(string content, string path)
        {
            File.WriteAllText(path, content);
        }
    }
}