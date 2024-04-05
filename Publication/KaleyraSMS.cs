using Newtonsoft.Json;
using System.Net;

namespace Publication;
public class KaleyraSMS
{
    //IHttpClientFactory _httpClientFactory;
    //public KaleyraSMS(IHttpClientFactory httpClientFactory)
    //{
    //    _httpClientFactory = httpClientFactory;
    //}
    public async Task<HttpStatusCode> GenerateOTPAsync(string mobileNo,string otp)
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
            //Log exception
            return HttpStatusCode.InternalServerError;
        }

        //try
        //{
        //    HttpClient client = _httpClientFactory.CreateClient("kaleyra");
        //    //2 minute timeout on wait for response
        //    client.Timeout = new TimeSpan(0, 2, 0);
        //    //Create an HttpRequestMessage object and pass it into SendAsync()
        //    HttpRequestMessage message = new HttpRequestMessage();
        //    message.Headers.Add("Accept", "application/json");
        //    message.Headers.Add("api-key", apiKey);
        //    message.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
        //    message.Method = HttpMethod.Post;
        //    message.RequestUri = new Uri(url);

        //    HttpResponseMessage response = await client.SendAsync(message);
        //    var result = await response.Content.ReadAsStringAsync();
        //    //deserialize the result into proper object type
        //    return  response.StatusCode;
        //}
        //catch (Exception ex)
        //{
        //    //Log exception
        //    return HttpStatusCode.InternalServerError;
        //}
    }
}
