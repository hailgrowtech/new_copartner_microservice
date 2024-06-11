using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;
using Microsoft.EntityFrameworkCore;
using Publication.Factory;
using static MassTransit.ValidationResultExtensions;
using System.Net;


namespace AdminDashboardService.Handlers;

public class CreateEmailStatusCommandHandler : IRequestHandler<CreateEmailStatusCommand, HttpStatusCode>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly eMailFactory _eMailFactory;
    public CreateEmailStatusCommandHandler(CoPartnerDbContext dbContext, eMailFactory eMailFactory)
    {
        _dbContext = dbContext;
        _eMailFactory = eMailFactory;
    }

    public async Task<HttpStatusCode> Handle(CreateEmailStatusCommand request, CancellationToken cancellationToken)
    {
        string[] cc;
            var entity = request.EmailStatus;
        if (entity.Cc.ToString().Length < 7 || !string.IsNullOrEmpty(entity.Cc))
            cc = new string[] { };
        else cc = new string[] { entity.Cc };
            // Send email
            var statusCode = await _eMailFactory.PostEmailAsync(
                new string[] { entity.To },
                cc,
                entity.Subject,
                entity.Body,
                entity.EmailFrom    // Make sure to pass email type if required
            );
            if (statusCode != HttpStatusCode.OK)
                entity.Status = "Error in Sending Email";
            else entity.Status = statusCode.ToString();

            await _dbContext.EmailStatus.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return statusCode;
    }

}