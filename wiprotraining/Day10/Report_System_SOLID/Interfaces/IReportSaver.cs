namespace Report_System.Interfaces
{
    // Interface responsible for saving report data
    public interface IReportSaver
    {
        void Save(string content, string path);
    }
}