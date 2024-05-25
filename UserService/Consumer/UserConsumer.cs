namespace UserService.Consumer;

using System.Threading.Tasks;
using Copartner;
using MassTransit;
using UserService.Logic;
using UserService.Profiles;

public class UserConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILogger<UserConsumer> _logger;
    private readonly IUserBusinessProcessor _logic;
    AutoMapperProfile userMapper = new AutoMapperProfile();
    public UserConsumer(ILogger<UserConsumer> logger, IUserBusinessProcessor logic)
    {
        this._logger=logger;
        this._logic=logic;
    }
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        _logger.LogInformation(" [*] Message received User: {MobileNumber} ,Name: {name} ", context.Message.MobileNumber);
        var userEntity = userMapper.ToUserCreateEntity(context.Message);
        await _logic.Post(userEntity);
       // return  Task.CompletedTask;
    }
}