using CommonLibrary.CommonModels;

namespace MigrationDB.Model
{
    public class APGeneratedLinks : BaseModel
    {
        public Guid? APId { get; set; }
        public string? GeneratedLink { get; set;}
        public string? APReferralLink { get; set;}
    }
}
