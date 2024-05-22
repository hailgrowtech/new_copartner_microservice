using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Commands;

public record DeleteBankUPICommand (Guid Id) : IRequest<WithdrawalMode>;
