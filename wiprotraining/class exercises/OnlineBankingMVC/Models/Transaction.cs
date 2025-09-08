public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; } // Deposit, Withdraw, Transfer
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
}
