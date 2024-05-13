using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("Join")]
    public class Join : BaseModel
    {
        public string ChannelName { get; set; }
        public string Link { get; set; }
        //public long Count { get; set; }
    }
}
