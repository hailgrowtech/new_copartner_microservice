using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MigrationDB.Model
{
    public class WebinarBooking : BaseModel
    {
        [Required]
        public Guid? WebinarId { get; set; }
        public Guid? UserId { get; set; }
    }
}
