using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("EmailStatus")]
    public class EmailStatus : BaseModel
    {
        public string EmailFrom { get; set; }
        public string? To { get; set; }
        public string? Cc { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? Status { get; set; }
        public string? Error { get; set; }
    }
}
