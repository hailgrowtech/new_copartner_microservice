using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
