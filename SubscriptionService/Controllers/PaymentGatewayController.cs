using CommonLibrary.Authorization;
using Microsoft.AspNetCore.Mvc;
using MigrationDB.Model;
using Publication.Factory;
using Razorpay.Api;

namespace SubscriptionService.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PaymentGatewayController : ControllerBase
{
    private readonly RazorpayClient _razorpayClient;

    public PaymentGatewayController(RazorpayConfiguration razorpayConfiguration)
    {
        _razorpayClient = razorpayConfiguration.RazorpayClient;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        var options = new Dictionary<string, object>
        {
            { "amount", request.Amount },
            { "currency", "INR" },
            { "receipt", "receipt#1" },
            { "payment_capture", 1 }
        };

        try
        {
            // Assuming _razorpayClient.Payment.Create(options) is the correct method to create a payment.
            // If this method does not exist, you'll need to consult the Razorpay SDK documentation for .NET.
            var payment = _razorpayClient.Payment.Create(options);
            return Ok(new { message = "Payment successful", transactionId = payment["id"] });
        }
        catch (Exception e) // Catch a general exception
        {
            // Log the exception if necessary
            return BadRequest(new { message = "Payment failed", error = e.Message });
        }
    }
}
