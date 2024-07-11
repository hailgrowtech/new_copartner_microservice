using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary;

public class AgoraAccessToken
{
    public string AppId { get; private set; }
    public string AppCertificate { get; private set; }
    public string ChannelName { get; private set; }
    public string Uid { get; private set; }
    public int ExpiredTs { get; private set; }

    public enum Privileges
    {
        kJoinChannel = 1,
        kPublishAudioStream = 2,
        kPublishVideoStream = 3,
        kPublishDataStream = 4
    }

    private readonly Dictionary<Privileges, int> _message = new Dictionary<Privileges, int>();

    public AgoraAccessToken(string appId, string appCertificate, string channelName, string uid)
    {
        AppId = appId;
        AppCertificate = appCertificate;
        ChannelName = channelName;
        Uid = uid;
    }

    public void AddPrivilege(Privileges privilege, int expireTs)
    {
        _message[privilege] = expireTs;
    }

    private string GenerateToken()
    {
        var timestamp = Convert.ToInt32(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        var salt = new Random().Next();
        var expiredTs = timestamp + ExpiredTs;

        var message = new StringBuilder();
        message.Append(AppId).Append(AppCertificate).Append(ChannelName).Append(Uid).Append(timestamp).Append(salt).Append(expiredTs);

        foreach (var item in _message)
        {
            message.Append((int)item.Key).Append(item.Value);
        }

        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(AppCertificate)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message.ToString()));
            return Convert.ToBase64String(hash);
        }
    }

    public string Build()
    {
        return GenerateToken();
    }
}
