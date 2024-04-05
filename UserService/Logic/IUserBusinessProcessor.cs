using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using UserService.Dtos;

namespace UserService.Logic;

public interface IUserBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(UserCreateDto user);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<UserCreateDto> userDto);
    bool ResetPassword(UserPasswordDTO userPasswordDTO);
}
