using AuthenticationService.DTOs;
using AuthenticationService.Models;
using CommonLibrary.CommonDTOs;

namespace AuthenticationService.Logic;
public interface IAuthenticationBusinessProcessor
{
    string GetUserIp();
    Task<bool> SaveUserAuthDetails(AuthenticationDetail userAuthDetails);
    Task<bool> SaveUserAuth(Authentication userAuth);
    Task<ResponseDto> Authenticate(AuthenticationRequestDTO model);
    Task<ResponseDto> RefreshToken(string token);
    void RevokeToken(string token);
    Task<ResponseDto> GetAuthUserById(Guid id);
}
