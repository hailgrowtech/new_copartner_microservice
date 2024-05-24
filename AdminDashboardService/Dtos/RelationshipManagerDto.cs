using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Dtos
{
    public class RelationshipManagerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }
        public string? DocumentPath { get; set; }
        public Experts Experts { get; set; }
        public AffiliatePartner AffiliatePartners { get; set; }
        public bool isActive { get; set; }
    }
}
