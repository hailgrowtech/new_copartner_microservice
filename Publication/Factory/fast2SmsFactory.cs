using System.Net;

namespace Publication.Factory;

public class fast2SmsFactory
{
    private readonly HttpClient _httpClient;

    public fast2SmsFactory(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.BaseAddress = new Uri("https://www.fast2sms.com/dev/bulkV2");
    }

    public async Task<HttpStatusCode> GenerateOTPAsync(string[] mobileNo, string otp)
    {
        try
        {
            string senderId = ConfigHelper.GetAppSettings().sender_id;
            string apiKey = ConfigHelper.GetAppSettings().apiKey;
            string messageId = ConfigHelper.GetAppSettings().message_id;

            var formData = new Dictionary<string, string>
            {
                { "sender_id", senderId },
                { "message", messageId },
                { "variables_values", otp },
                { "route", "dlt" },
                { "numbers", string.Join(",", mobileNo) }
            };

            var content = new FormUrlEncodedContent(formData);
            _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);

            var response = await _httpClient.PostAsync("", content);
            return response.StatusCode;
        }
        catch (Exception ex)
        {
            // Log exception
            return HttpStatusCode.InternalServerError;
        }
    }
}