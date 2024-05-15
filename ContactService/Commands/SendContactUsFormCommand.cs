using ContactService.Dto;
using MediatR;
using MigrationDB.Models;

namespace ContactService.Commands;

public record SendContactUsFormCommand(ContactUsFormDto form) : IRequest<ContactUsFormDto>;
