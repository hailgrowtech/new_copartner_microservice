using AuthenticationService.DTOs;
using AuthenticationService.Models;

namespace AuthenticationService.Profiles;

public class AuthMapperProfile
{
    //ToDo Implement Automapper TechDebt
    public AuthenticationDetail ToCreateAuthDetailEntity(UserCreatedEventDTO user)
    {
        string password = user.Password;
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new AuthenticationDetail
        {
            UserId = user.UserId,
            Email = user.Email,
            MobileNumber = user.Mobile,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt)
        };

        return result;
    }

    public Authentication ToCreateAuthEntity(UserCreatedEventDTO user)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new Authentication
        {
            UserId = user.UserId,
            PasswordSalt = salt
        };

        return result;
    }

    public AuthenticationRequestDTO ToCreateAuthRequestDto(UserCreatedEventDTO user)
    {
        string password = user.Password;
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        var result = new AuthenticationRequestDTO
        {
            Mobile = user.Mobile,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt),
            IsLoginUsingOtpRequest = true,
            UserIpAddress = string.Empty,
            OTP = "1234"
        };

        return result;
    }
}

