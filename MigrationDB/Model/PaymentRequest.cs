namespace MigrationDB.Model
{
    public class PaymentRequest
    {
        public string MerchantId { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CallbackUrl { get; set; }
        public string CustomerId { get; set; }
    }

}
