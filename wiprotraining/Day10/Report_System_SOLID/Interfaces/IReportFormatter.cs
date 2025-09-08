namespace Report_System.Interfaces
{
    // Interface for formatting strategies (Open/Closed Principle)
    public interface IReportFormatter
    {
        string Format(string content);
    }
}