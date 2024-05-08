using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    public class PaymentRequest
    {
        [Precision(18, 2)]
        public decimal Amount { get; set; }

    }

}
