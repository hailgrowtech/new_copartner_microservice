using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WalletService.Logic;
using WalletService.Dtos;

namespace WalletService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WithdrawalController : ControllerBase
{
    private readonly IWithdrawalBusinessProcessor _logic;
    private readonly ILogger<WithdrawalController> _logger;

    public WithdrawalController(IWithdrawalBusinessProcessor logic, ILogger<WithdrawalController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    /// <summary>
    /// Gets the list of all Withdrawal In Dashboard.
    /// </summary>
    /// <returns>The list of Withdrawal.</returns>
    // GET: api/GetBankUPIDetails
    [HttpGet]
    public async Task<object> Get(string RequestBy,int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Withdrawal Data..");
        var experts = await _logic.Get(RequestBy,page, pageSize);
        return Ok(experts);
    }
/// <summary>
/// Get List Of All Bank and UPI saved
/// </summary>
/// <returns></returns>
    [HttpGet("GetBankUPIDetails", Name = "GetBankUPIDetails")]
    public async Task<object> GetBankUPIDetails()
    {
        _logger.LogInformation("Fetching Bank/UPI Data..");
        var experts = await _logic.GetWithdrawalMode();
        return Ok(experts);
    }


    /// <summary>
    /// Get withdrawal/transaction details in Dashboard
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Withdrawal/8AE6AF68-05FC-438D-3FA2-08DC70094B91
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("{Id}")]
    public async Task<ActionResult<WithdrawalReadDto>> Get(Guid Id)
    {
        _logger.LogInformation("Fetching withdrawal details for Id : " + Id.ToString());
        var withdrawal = await _logic.Get(Id);
        return withdrawal != null ? (ActionResult<WithdrawalReadDto>)Ok(withdrawal) : NotFound();
    }

    /// <summary>
    /// Get Bank/UPI Details By ID.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/GetBankUPIById/1
    /// </remarks>
    /// <param name="Id"></param>
    [HttpGet("GetBankUPIById/{Id}", Name = "GetBankUPIById")]
    public async Task<ActionResult<WithdrawalModeReadDto>> GetBankUPIById(Guid Id)
    {
        _logger.LogInformation("Fetching bank upi details for Id : " + Id.ToString());
        var blogs = await _logic.GetWithdrawalMode(Id);
        return blogs != null ? (ActionResult<WithdrawalModeReadDto>)Ok(blogs) : NotFound();
    }



    /// <summary>
    /// Get Bank/UPI Details by RA/AP Id
    /// </summary>
    ///   <remarks>
    /// Sample request:
    /// 
    ///     GET : api/GetBankUPIByUserId/1
    /// </remarks>
    /// <param name="Id"></param>
    /// <param name="userType"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("BankUPIByUserId/{Id}", Name = "GetBankUPIByUserId")]
    public async Task<ActionResult<WithdrawalModeReadDto>> GetBankUPIByUserId(Guid Id, string userType="RA", int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching bank upi details for user Id : " + Id.ToString());
        var blogs = await _logic.GetWithdrawalModeByUserId(Id,userType,page,pageSize);
        return blogs != null ? (ActionResult<WithdrawalModeReadDto>)Ok(blogs) : NotFound();
    }

    [HttpGet("GetWithdrawalByUserId/{Id}", Name = "GetWithdrawalByUserId")]
    public async Task<ActionResult<WithdrawalReadDto>> GetWithdrawalByUserId(Guid Id, string userType = "RA", int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching bank upi details for Id : " + Id.ToString());
        var blogs = await _logic.GetWithdrawalModeByUserId(Id, userType, page, pageSize);
        return blogs != null ? (ActionResult<WithdrawalReadDto>)Ok(blogs) : NotFound();
    }

    /// <summary>
    /// Create Withdrawal Request From AP/RA Screen Check Withdrawal Balance before making request using API api/Wallet/GetWalletWithdrawalBalance . 
    /// </summary>
    /// /// <remarks>
    /// Sample request:
    /// 
    /// POST : api/Post For Action Put Pending 'P'
    /// </remarks>
    /// <param name="bankUPIDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<object> Post(WithdrawalCreateDto bankUPIDto)
    {
        var response = await _logic.Post(bankUPIDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    /// <summary>
    /// Create Data for Bank/UPI - Adding Bank and UPI
    /// </summary>
    /// /// <remarks>
    /// Sample request:
    /// 
    /// POST : api/PostBankUPIDetails
    /// </remarks>
    /// <param name="bankUPIDto"></param>
    /// <returns></returns>
    [HttpPost("PostBankUPIDetails", Name = "PostBankUPIDetails")]
    public async Task<object> PostBankUPIDetails(WithdrawalModeCreateDto bankUPIDto)
    {
        var response = await _logic.PostWithdrawalMode(bankUPIDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    /// <summary>
    /// Action / Reason & TransactionID for Withdrawal Request In Dashboard
    /// </summary>
    /// /// <remarks>
    /// Sample request:
    /// 
    /// PUT : api/Put: For Reject - 'R', Accept - 'A' , Pending 'P'
    /// </remarks>
    /// <param name="bankUPIDto"></param>
    /// <returns></returns>
    [HttpPut("{Id:guid}")]
    public async Task<object> Put(Guid Id, WithdrawalCreateDto withdrawalDto)
    {
        var response = await _logic.Put(Id, withdrawalDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    /// <summary>
    /// Change UPI/ Bank Details if any
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="withdrawalModeCreateDto"></param>
    /// <returns></returns>
    [HttpPut("PutBankUPIDetails/{Id}", Name = "PutBankUPIDetails")]
    public async Task<object> PutBankUPIDetails(Guid Id, WithdrawalModeCreateDto withdrawalModeCreateDto)
    {
        var response = await _logic.PutBankUPIDetails(Id, withdrawalModeCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }
    /// <summary>
    /// Delete Bank UPI Detail By Withdrawal Mode ID
    /// </summary>
    /// <param name="Id">Bank UPI Mode Id </param>
    /// <returns></returns>
    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> Delete(Guid Id)
    {
        var expert = await _logic.Delete(Id);
        return expert != null ? Ok(expert) : NotFound();
    }
}
 