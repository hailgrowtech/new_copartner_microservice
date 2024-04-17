using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using UserService.Dtos;

namespace UserService.Logic;

public interface IUserBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(UserCreateDto user);
    Task<ResponseDto> Put(Guid id, UserCreateDto subscriberCreateDto);

    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<UserCreateDto> userDto);
    Task<ResponseDto> Delete(Guid id);
    bool ResetPassword(UserPasswordDTO userPasswordDTO);
}
