using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models;
[Table("User")]
public class User : BaseModel
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
}
