using CommonLibrary.CommonModels;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthenticationService.Models;
[Table("ForgotPassword")]
public class ForgotPassword : BaseModel
{
    public Guid? UserId { get; set; }
    public string? UserType { get; set; }
    public string Email{ get; set; }
    [JsonIgnore]
    public string? Token { get; set; }
    public DateTime Expires { get; set; }
    public bool?  IsValidated { get; set; }
}
