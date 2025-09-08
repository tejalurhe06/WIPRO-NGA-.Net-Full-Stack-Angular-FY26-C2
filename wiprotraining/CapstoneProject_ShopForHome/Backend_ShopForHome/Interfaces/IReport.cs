namespace ShopForHome.API.Interfaces
{
    public interface IReportsService
    {
        Task<object> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<List<object>> GetLowStockReportAsync();
    }
}
