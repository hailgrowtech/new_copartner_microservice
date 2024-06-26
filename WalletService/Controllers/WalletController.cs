﻿using Microsoft.AspNetCore.Mvc;
using WalletService.Dtos;
using WalletService.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WalletService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletBusinessProcessor _logic;
    private readonly ILogger<WalletController> _logger;

    public WalletController(IWalletBusinessProcessor logic, ILogger<WalletController> logger)
    {
        _logic = logic;
        _logger = logger;
    }
    /// <summary>
    /// Gets the list of Wallet.
    /// </summary>
    /// <returns>The list of Withdrawal.</returns>
    // GET: api/GetBankUPIDetails
    [HttpGet]
    public async Task<object> Get(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching Wallet Data..");
        var experts = await _logic.Get(page, pageSize);
        return Ok(experts);
    }

    /// <summary>
    /// Get Wallet Balance BY RA AP Id Used in RA AP Screen for shwoing wallet balance and withdrawal balance.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET : api/Wallet/1
    /// </remarks>
    /// <param name="Id">RA/AP Id</param>
    /// /// <param name="userType">RA or AP</param>
    [HttpGet("GetWalletWithdrawalBalance/{Id}", Name = "GetWalletWithdrawalBalance")]
    public async Task<ActionResult<WalletWithdrawalReadDto>> GetWalletWithdrawalBalance(Guid Id, string userType)
    {
        _logger.LogInformation("Fetching wallet withdrawal balance details for Id : " + Id.ToString());
        var wallet = await _logic.Get(Id,userType);
        return wallet != null ? (ActionResult<WalletWithdrawalReadDto>)Ok(wallet) : NotFound();
    }

    [HttpPost]
    public async Task<object> Post(WalletCreateDto walletCreateDto)
    {
        var response = await _logic.Post(walletCreateDto);

        if (response.IsSuccess)
        {
            Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }

    // PUT api/<WalletController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<WalletController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
