using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonLibrary.Authorization;
using ContactUsService.Dto;
using ContactService.Logic;
using ContactService.Dto;

namespace ContactUsService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactUsController : ControllerBase
{
    private readonly IContactUsBusinessProcessor _logic;
    private readonly ILogger<ContactUsController> _logger;

    public ContactUsController(IContactUsBusinessProcessor logic, ILogger<ContactUsController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitContactUsForm(ContactUsFormDto formDto)
    {
        _logger.LogInformation("Submitting Contact Us form..");
        var response = await _logic.Post(formDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
}
