using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.CommonDTOs;
public class RabbitMqConfig
{
    public string Hostname { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
