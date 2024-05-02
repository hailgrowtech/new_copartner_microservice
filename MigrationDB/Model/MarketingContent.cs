using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("MarketingContent")]
    public class MarketingContent : BaseModel
    {
        public string BannerName { get; set; }
        public string? ImagePath { get; set; }
        public string? ContentType { get; set; }
    }
}


