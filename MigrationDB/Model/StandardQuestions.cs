using CommonLibrary.CommonModels;

namespace MigrationDB.Model
{
    public class StandardQuestions : BaseModel
    {
        public Guid? ExpertId { get; set; }
        public string? ServiceType { get; set; }
        public string? ChatId { get; set; }
        public string? Question {  get; set; }
        public string? Answer { get; set; }
    }
}
