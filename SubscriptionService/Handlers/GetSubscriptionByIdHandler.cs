﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, Subscription>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscription> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var subscriptionMstsList = await _dbContext.Subscriptions.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return subscriptionMstsList;
        }
    }

}
