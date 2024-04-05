using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary;
public static class IpHelper
{
    public static string GetUserIp()
    {
        string hostName = Dns.GetHostName();
        return Dns.GetHostEntry(hostName).AddressList[0].ToString();
    }
    public static string GetUserMacId()
    {
        return "Some MAc ID";
    }
}
