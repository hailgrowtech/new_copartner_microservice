namespace WalletService.Consumer;

using System.Threading.Tasks;
using Copartner;
using MassTransit;
using WalletService.Logic;
using WalletService.Profiles;

public class WalletConsumer : IConsumer<WalletEvent>
{
    private readonly ILogger<WalletConsumer> _logger;
    private readonly IWalletBusinessProcessor _logic;
    AutoMapperProfile userMapper = new AutoMapperProfile();
    public WalletConsumer(ILogger<WalletConsumer> logger, IWalletBusinessProcessor logic)
    {
        this._logger=logger;
        this._logic=logic;
    }
    public async Task Consume(ConsumeContext<WalletEvent> context)
    {
        _logger.LogInformation(" [*] Message received ");
        var walletEntity = userMapper.ToWalletCreateEntity(context.Message);
        await _logic.Post(walletEntity);
    }
}