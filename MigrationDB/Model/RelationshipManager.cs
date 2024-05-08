using CommonLibrary.CommonModels;
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
    }

}
