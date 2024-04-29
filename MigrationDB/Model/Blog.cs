using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("Blog")]
    public class Blog : BaseModel
    {
        public string Title { get; set; }
        public string? BlogImagePath { get; set; }
        public string? Description { get; set; }
    }
}
