using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_System
{
    public class ReportManager
    {
        public string GenerateReport()
        {
            // Generate report logic
            return "Report Content";
        }

        public void SaveReport(string content)
        {
            File.WriteAllText("report.txt", content);
        }
    }

    public class ReportFormatter
    {
        public string Format(string content, string type)
        {
            if (type == "PDF")
                return $"PDF: {content}";
            else if (type == "Excel")
                return $"Excel: {content}";
            return content;
        }
    }

}
