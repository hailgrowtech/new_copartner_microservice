using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{

    [Table("RelationshipManager")]
    public class RelationshipManager : BaseModel
    {
        public string Name { get; set; }
        public string Mobile { get; set;  }
        public string Email { get; set; }    
        public string? ImagePath { get; set; }
        public string? DocumentPath { get; set; }
        public Experts Experts { get; set; }
        public AffiliatePartner AffiliatePartners { get; set; }
        public bool isActive { get; set; }

    }

}
