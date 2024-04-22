using CommonLibrary.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDB.Model;
using Publication.Factory;

namespace SubscriptionService.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PaymentGatewayController : ControllerBase
{
    private readonly paymentGatewayFactory _paymentGatewayFactory;

    public PaymentGatewayController(paymentGatewayFactory paymentGatewayFactory)
    {
        _paymentGatewayFactory = paymentGatewayFactory;
    }

    [HttpPost("initiate")]
    public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequest paymentRequest)
    {
        try
        {
            // Use the PhonePePaymentService to initiate the payment
            var transactionId = await _paymentGatewayFactory.InitiatePaymentAsync(paymentRequest);

            // Return the transaction ID in the response
            return Ok(new { TransactionId = transactionId });
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return BadRequest(new { Error = ex.Message });
        }
    }

}
