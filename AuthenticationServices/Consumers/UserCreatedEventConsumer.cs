using AuthenticationService.DTOs;
using AuthenticationService.Logic;
using AuthenticationService.Profiles;
using MassTransit;

namespace AuthenticationService.Consumers;
internal class UserCreatedEventConsumer : IConsumer<UserCreatedEventDTO>
{
    AuthMapperProfile authMapper = new AuthMapperProfile();

    private readonly IAuthenticationBusinessProcessor _logic;
    public UserCreatedEventConsumer(IAuthenticationBusinessProcessor authenticationBusinessProcessor)
        => this._logic = authenticationBusinessProcessor;
    public async Task Consume(ConsumeContext<UserCreatedEventDTO> context)
    {
        var authDetailEntity = authMapper.ToCreateAuthDetailEntity(context.Message);//.ToCreateAuthDetailEntity();
        var authDetails = await _logic.SaveUserAuthDetails(authDetailEntity);
        //  var message = context.Message.UserId;
        var authEntity = authMapper.ToCreateAuthEntity(context.Message);//.ToCreateAuthDetailEntity();
        var auth = await _logic.SaveUserAuth(authEntity);

        //send token
      //  var tokenEntity = authMapper.ToCreateAuthRequestDto(context.Message);
      //  var tokenObj = await _logic.Authenticate(tokenEntity);

        // var abc =  Task.FromResult(tokenObj);
      //  return Task.FromResult(tokenObj);

       // return Task.CompletedTask;
    }
}

    

