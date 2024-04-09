using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;
[Table("User")]
public class User : BaseModel
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string SubscriptionType { get; set; }
    public string SubscriptionId { get; set; }
}
