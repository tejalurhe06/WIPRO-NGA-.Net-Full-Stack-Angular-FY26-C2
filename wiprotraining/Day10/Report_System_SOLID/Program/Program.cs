using System;
using Report_System.Models;
using Report_System.Interfaces;
using Report_System.Services;
using Report_System.Formatters;

namespace Report_System.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example using SalesReport with PDF formatting
            Report report = new SalesReport();
            IReportFormatter formatter = new PdfFormatter();
            IReportSaver saver = new ReportSaver();
            var generator = new ReportGenerator(report);
            var service = new ReportService(formatter, saver, generator);

            service.ProcessReport("SalesReport.txt");

            Console.WriteLine("Sales Report processed and saved.");

            // Example using InventoryReport with Excel formatting
            report = new InventoryReport();
            formatter = new ExcelFormatter();
            generator = new ReportGenerator(report);
            service = new ReportService(formatter, saver, generator);

            service.ProcessReport("InventoryReport.txt");

            Console.WriteLine("Inventory Report processed and saved.");
        }
    }
}