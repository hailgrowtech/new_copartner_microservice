using CommonLibrary.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Dtos;
using SubscriptionService.Logic;

namespace SubscriptionService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PaymentResponseController : ControllerBase
{
    private readonly IPaymentResponseBusinessProcessor _logic;
    private readonly ILogger<PaymentResponseController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public PaymentResponseController(IPaymentResponseBusinessProcessor logic, ILogger<PaymentResponseController> logger)//, ITopicProducer<UserCreatedEventDTO> topicProducer)
    {
        this._logic = logic;
        this._logger = logger;
        // this._topicProducer = topicProducer;
    }
    /// <summary>
    /// Gets the list of all Payment Responses.
    /// </summary>
    /// <returns>The list of Experts.</returns>
    // GET: api/PaymentResponse
    [HttpGet]
    public async Task<object> Get()
    {
        _logger.LogInformation("Fetching Payment Response Data..");
        var payment = await _logic.Get();
        return Ok(payment);
    }

    /// <summary>
    /// Get Payment Response.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Experts/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}")]
    public async Task<ActionResult<PaymentResponseReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching payment response details for Id : " + Id.ToString());
        var paymentMsts = await _logic.Get(Id);
        return paymentMsts != null ? (ActionResult<PaymentResponseReadDto>)Ok(paymentMsts) : NotFound();
    }

    /// <summary>
    /// Gets Payment By Status P - Pending, F - failure , S - Success.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/GetPaymentByStatus/1
    /// </remarks>
    /// <param name="Id"></param>
    /// /// <param name="paymentStatus">Status P - Pending, F - failure , S - Success</param>
    [HttpGet("GetPaymentByStatus", Name = "GetPaymentByStatus")]
    public async Task<ActionResult<PaymentResponseReadDto>> GetPaymentByStatus(string paymentStatus = "P")
    {
        _logger.LogInformation("Fetching payment by status : ");
        var payments = await _logic.Get(paymentStatus);
        return payments != null ? (ActionResult<PaymentResponseReadDto>)Ok(payments) : NotFound();
    }

    /// <summary>
    /// Post Payment Response By Status P - Pending, F - failure , S - Success.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     Post : api/GetPaymentByStatus
    /// </remarks>
    /// <param name="Id"></param>
    /// /// <param name="paymentStatus">Status P - Pending, F - failure , S - Success</param>
    [HttpPost]
    public async Task<object> Post(PaymentResponseCreateDto paymentResponseCreateDto)
    {
        var response = await _logic.Post(paymentResponseCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id,PaymentResponseCreateDto paymentResponseCreateDto)
    {
        var response = await _logic.Put(Id, paymentResponseCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPatch]
    public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<PaymentResponseCreateDto> paymentResponseDtoPatch)
    {
        var response = await _logic.Patch(Id, paymentResponseDtoPatch);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var user = await _logic.Delete(Id);
        return user != null ? Ok(user) : NotFound();
    }
}
