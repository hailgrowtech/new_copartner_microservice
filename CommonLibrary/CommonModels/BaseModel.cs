using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CommonLibrary.CommonModels;

public class BaseModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; } = new Guid();
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = new DateTime();
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }

    [DefaultValue("false")]
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}

