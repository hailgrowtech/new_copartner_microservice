﻿using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record CreateAPTransactionsDetailsCommand(APTransactionsDetailsDto APTransactionsDetailsDto) : IRequest<APTransactionsDetailsDto>;