using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication;
public class AppSettings
{
    public string SID { get; set; }

    // refresh token time to live (in days), inactive tokens are
    // automatically deleted from the database after this time
    public string apiKey { get; set; }
    public string sender { get; set; }
    public string template_id { get; set; }
}
