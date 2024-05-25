using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Commands;
public record CreateWalletCommand(Wallet Wallet) : IRequest<Wallet>;

