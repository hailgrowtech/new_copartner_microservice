using CommonLibrary.CommonModels;
using System.ComponentModel;

namespace MigrationDB.Model
{
    public class APGeneratedLinks : BaseModel
    {
        public Guid? APId { get; set; }
        public string? GeneratedLink { get; set;}
        public string? APReferralLink { get; set;}
        public string? Tag { get; set; }
        [DefaultValue("false")]
        public bool? isArchive { get; set; }
    }
}
