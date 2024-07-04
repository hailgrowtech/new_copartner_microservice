﻿using MediatR;
using ExpertService.Commands;
using ExpertService.Profiles;
using MigrationDB.Models;
using MigrationDB.Data;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.CommonModels;
using ExpertsService.Commands;
using MigrationDB.Model;
namespace ExpertsService.Handlers
{
    public class PatchWebinarBookingHandler : IRequestHandler<PatchWebinarBookingCommand, WebinarBooking>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;

        public PatchWebinarBookingHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<WebinarBooking> Handle(PatchWebinarBookingCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentWebinar = await _dbContext.WebinarBookings.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentWebinar == null)
            {
                // Handle the case where the expert does not exist
                throw new Exception($"Webinar Booking with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var webinarToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentWebinar);
            webinarToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.WebinarBookings.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(webinarToUpdate);
            _dbContext.Entry(webinarToUpdate).State = EntityState.Modified;

            // Preserve multiple properties 
            _dbContext.PreserveProperties(webinarToUpdate, currentWebinar, "CreatedOn");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return webinarToUpdate;
        }
    }
}
