using CommonLibrary.CommonModels;

namespace MigrationDB.Model
{
    public class MinisubscriptionLink : BaseModel
    {
        public Guid? ExpertId { get; set; }
        public Guid? SubscriptionId { get; set; }
        public string? MiniSubscriptionLink { get; set; }
    }
}
