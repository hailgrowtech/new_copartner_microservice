namespace MigrationDB.Model
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
