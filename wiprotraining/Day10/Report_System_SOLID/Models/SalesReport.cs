using Report_System.Models;

namespace Report_System.Models
{
    // Derived class representing a sales report
    public class SalesReport : Report
    {
        public override string GetContent()
        {
            return "Sales Report Content";
        }
    }
}