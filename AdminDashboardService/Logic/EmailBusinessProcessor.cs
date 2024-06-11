using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;
using AdminDashboardService.Queries;
using AdminDashboardService.Commands;
using System.Net;

namespace AdminDashboardService.Logic;
public class EmailBusinessProcessor : IEmailBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public EmailBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

   
    public async Task<ResponseDto> Post(EmailCreateDto request)
    {
        var emails = _mapper.Map<EmailStatus>(request);
        // Send the command
        var result = await _sender.Send(new CreateEmailStatusCommand(emails));
        if (result != HttpStatusCode.OK)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = emails,
                ErrorMessages = new List<string>() { "Unable to send email" }
            };
        }
        return new ResponseDto()
        {
            IsSuccess = true,
            DisplayMessage = "Email sent successfully."
        };
    }

}

