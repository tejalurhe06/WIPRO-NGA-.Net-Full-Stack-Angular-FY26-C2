using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_System.Interfaces
{
    public interface IReportFormatter
    {
        string Format(string content);
    }
}
