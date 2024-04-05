using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.CommonDTOs;
public class ResponseDto
{
    public bool IsSuccess { get; set; } = true;
    public object Data { get; set; } = null;
    public string DisplayMessage { get; set; } = "";
    public List<string> ErrorMessages { get; set; }
    public int ResponseStatusCode { get; set; }
}
