using MigrationDB.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Publication.Factory
{
    public class paymentGatewayFactory
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public paymentGatewayFactory(HttpClient httpClient, string apiKey, string apiSecret)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }


        public async Task<string> InitiatePaymentAsync(PaymentRequest paymentRequest)
        {
            try
            {
                // Prepare your request body
                var requestBody = JsonConvert.SerializeObject(paymentRequest);
                var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(requestBody));

                // Calculate the checksum
                var saltKey = "099eb0cd-02cf-4e2a-8aca-3e6c6aff0399"; // Sample salt key
                var saltIndex = "1"; // Sample salt index
                var checksum = CalculateChecksum(base64Payload, saltKey, saltIndex);

                // Add the checksum to the request headers
                _httpClient.DefaultRequestHeaders.Add("X-Verify", checksum);

                // Make the request to PhonePe API
                var response = await _httpClient.PostAsync("https://<PhonePe API URL>/pg/v1/pay", new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                // Parse the response to get the transaction ID
                var responseBody = await response.Content.ReadAsStringAsync();
                var paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(responseBody);

                // Return the transaction ID
                return paymentResponse.TransactionId;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Failed to initiate payment. Error: {ex.Message}");
            }

        }




        public string CalculateChecksum(string base64Payload, string saltKey, string saltIndex)
        {
            // Concatenate the payload, API endpoint, and salt key
            var concatenatedString = $"{base64Payload}/pg/v1/pay{saltKey}";

            // Calculate the SHA256 hash of the concatenated string
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(concatenatedString);
                var hash = sha256.ComputeHash(bytes);
                var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();

                // Append "###" and the salt index to the hash
                return $"{hashString}###{saltIndex}";
            }
        }
    }
}
