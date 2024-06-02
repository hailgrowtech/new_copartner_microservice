using AuthenticationService.Dtos;
using AuthenticationService.DTOs;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace AuthenticationService.Profiles;

public class AuthMapperProfile :Profile
{
    public AuthMapperProfile()
    {
        // Source -> Target
        CreateMap<AuthenticationDetail, UserReadDto>().ReverseMap();
        CreateMap<AuthenticationDetail, UserCreateDto>().ReverseMap();
        CreateMap<AuthenticationDetail, UserPasswordDTO>().ReverseMap();
        CreateMap<AuthenticationDetail, JsonPatchDocument<UserCreateDto>>().ReverseMap();

        CreateMap<ForgotPassword, ForgotPasswordDTO>().ReverseMap();
    }
    //ToDo Implement Automapper TechDebt
    public AuthenticationDetail ToCreateAuthDetailEntity(UserCreateDto user)
    {
        string password = user.Password;
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new AuthenticationDetail
        {
            UserType = user.UserType,
            StackholderId = (Guid)user.StackholderId,
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            MobileNumber = user.MobileNo,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt),
            IsActive = user.IsActive
        };

        return result;
    }

    public Authentication ToCreateAuthEntity(UserCreateDto user)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new Authentication
        {
            UserId = (Guid)user.UserId,
            PasswordSalt = salt
        };

        return result;
    }

    public AuthenticationRequestDTO ToCreateAuthRequestDto(UserCreateDto user)
    {
        string password = user.Password;
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new AuthenticationRequestDTO
        {
            Mobile = user.MobileNo,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt),
            IsLoginUsingOtpRequest = true,
            UserIpAddress = string.Empty,
           // OTP = "1234"
        };

        return result;
    }
}

