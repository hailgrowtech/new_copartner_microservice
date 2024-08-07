﻿using MigrationDB.Model;
using MigrationDB.Models;

namespace SubscriptionService.Dtos
{
    public class SubscriberCreateDto
    {
        
        public Guid SubscriptionId { get; set; }


        public Guid UserId { get; set; }

        public decimal GSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string PaymentMode { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }

        public bool isActive { get; set; }
        public string? PremiumTelegramChannel { get; set; }

    }
}
