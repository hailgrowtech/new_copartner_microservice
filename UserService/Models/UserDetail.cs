using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models;

[Table("UserDetail")]
public class UserDetail : BaseModel
{
    public bool? Gender { get; set; }
    public string? PanCard { get; set; }
    public string? AadharCard { get; set; }  
}
