using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;
using System.Net;

namespace AdminDashboardService.Commands;

public record CreateEmailStatusCommand(EmailStatus EmailStatus) : IRequest<HttpStatusCode>;

