using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_System.Helpers
{
    public class ReportSaver
    {
        public void SaveToFile(string content, string filePath)
        {
            File.WriteAllText(filePath, content);
        }
    }
}
