using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ExpertService.Dtos;
using ExpertService.Logic;
using CommonLibrary.Authorization;
using ExpertsService.Logic;
using EmailService.Logic;
using EmailService.Dtos;

namespace ExpertService.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailBusinessProcessor _logic;
    private readonly ILogger<EmailController> _logger;
    //private readonly ITopicProducer<UserCreatedEventDTO> _topicProducer;

    public EmailController(IEmailBusinessProcessor logic, ILogger<EmailController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _logic.SendEmailAsync(request.To, request.Subject, request.Body);
        return Ok(new { message = "Email sent successfully" });
    }



}
