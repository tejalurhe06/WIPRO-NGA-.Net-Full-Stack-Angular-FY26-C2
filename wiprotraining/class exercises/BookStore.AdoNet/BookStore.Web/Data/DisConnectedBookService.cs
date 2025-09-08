using System.Data;
namespace BookStore.Web.Data
{
    public class DisconnectedBookService
    {
        private readonly IBookRepository _repo;
        private readonly ILogger<DisconnectedBookService> _logger;
        public DisconnectedBookService(IBookRepository repo,
        ILogger<DisconnectedBookService> logger)
        {
            _repo = repo; _logger = logger;
        }
        // Example: increase all prices by a percent in-memory (DataTable) then push changes
        public async Task<int> IncreaseAllPricesAsync(decimal pct,
        CancellationToken ct = default)
        {
            var ds = await _repo.GetAllDataSetAsync(ct);
            var table = ds.Tables["Books"]!;
            foreach (DataRow row in table.Rows)
            {
                var old = Convert.ToDecimal(row["Price"]);
                row["Price"] = Math.Round(old * (1 + pct), 2);
            }
            return await _repo.UpdateViaDataSetAsync(ds, ct);
        }
    }
}