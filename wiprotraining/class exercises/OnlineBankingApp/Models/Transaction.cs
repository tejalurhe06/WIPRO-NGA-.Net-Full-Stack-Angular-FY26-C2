public class Transaction
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; } // Deposit, Withdrawal, Transfer
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
}
