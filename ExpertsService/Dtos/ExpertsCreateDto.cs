﻿using ExpertService.Models;
using System.Numerics;

namespace ExpertService.Dtos
{
    public class ExpertsCreateDto
    {
        public string Name { get; set; }
        //public ExpertsType ExpertType { get; set; }
        public string? ExpertImagePath { get; set; }
        public int? ExpertTypeId { get; set; }
        public string? SEBIRegNo { get; set; }
        public string? Email { get; set; }
        public int? Experience { get; set; }
        public int? Rating { get; set; }
        public string? MobileNumber { get; set; }

        public string? ChannelName { get; set; }
        public string? TelegramChannel { get; set; }
        public string? PremiumTelegramChannel { get; set; }

        public int? TelegramFollower { get; set; }
        public bool isCoPartner { get; set; }
    }
}
