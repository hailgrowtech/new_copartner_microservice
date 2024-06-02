using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication;
public class AppSettings
{

    // refresh token time to live (in days), inactive tokens are
    // automatically deleted from the database after this time
    public string apiKey { get; set; }
    public string sender_id { get; set; }
    public string message_id { get; set; }

    //For Sending Email
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
