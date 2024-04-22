using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.Factory
{
    public class RazorpayConfiguration
    {
        public RazorpayClient RazorpayClient { get; }

        public RazorpayConfiguration(IConfiguration configuration)
        {
            var keyId = configuration["Razorpay:KeyId"];
            var keySecret = configuration["Razorpay:KeySecret"];

            RazorpayClient = new RazorpayClient(keyId, keySecret);
        }
    }
}
