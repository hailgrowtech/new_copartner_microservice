using System.Net;

namespace Publication.Factory;

public class SmsFactory
{
    public async Task<HttpStatusCode> GenerateOTPAsync(string mobileNo, string otp)
    {
        try
        {
            string sid = ConfigHelper.GetAppSettings().SID;
            string apiKey = ConfigHelper.GetAppSettings().apiKey;
            string sender = ConfigHelper.GetAppSettings().sender;
            string templateID = ConfigHelper.GetAppSettings().template_id;

            var url = "https://api.kaleyra.io/v1/" + sid + "/messages";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Headers["api-key"] = apiKey;
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            // var client = _httpClientFactory.CreateClient("kaleyra");
            var data = "to=" + mobileNo + "&sender=" + sender + "&source=API&type=OTP&body=" + otp + " is the One Time Verification(OTP) for phone no. verification at hailgrotech.com.&template_id=" + templateID + "";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return httpResponse.StatusCode;
        }
        catch (Exception ex)
        {
            // perform logging
            return HttpStatusCode.InternalServerError;

        }
    }
}