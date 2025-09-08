using Report_System.Models;

namespace Report_System.Models
{
    // Derived class representing an inventory report
    public class InventoryReport : Report
    {
        public override string GetContent()
        {
            return "Inventory Report Content";
        }
    }
}