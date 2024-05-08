using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;
using WalletService.Dtos;


namespace WalletService.Queries;
public record GetWalletByIdQuery(Guid Id, string userType) : IRequest<WalletWithdrawalReadDto>;


 
