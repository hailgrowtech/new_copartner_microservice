using AdminDashboardService.Logic;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using CommonLibrary.CommonDTOs;
using CommonLibrary;
using AdminDashboardService.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminDashboardService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailBusinessProcessor _logic;
    private readonly ILogger<EmailController> _logger;
    private readonly IConfiguration _configuration;

    public EmailController(IEmailBusinessProcessor logic, ILogger<EmailController> logger)
    {
        _logic = logic;
        _logger = logger;
    }
    /// <summary>
    /// Send Email To Support / Withdrawal / Onboarding
    /// </summary>
    /// <param name="file"></param>
    /// <param name="prefix">Name of Folder For Images user Images for video use Videos</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(EmailCreateDto emailCreateDto)
    {
        var response = await _logic.Post(emailCreateDto);
        // Handle the result
        if (response.IsSuccess)
        {
            // Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

            return Ok(response);
        }
        return NotFound(response);
    }
}
