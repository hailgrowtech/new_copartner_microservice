using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AuthenticationService.Dtos;

namespace AuthenticationService.Logic;

public interface IUserBusinessProcessor
{
    Task<ResponseDto> Get(string userType, int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(UserCreateDto stackholder);
    Task<ResponseDto> Put(Guid id, UserCreateDto subscriberCreateDto);

    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<UserCreateDto> stackholderDto);
    Task<ResponseDto> Delete(Guid id);
    Task<ResponseDto> ResetPassword(UserPasswordDTO userPasswordDTO);
    Task<ResponseDto> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
}
