using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary;
public static class AppConstants
{
    #region Session and Cookies Variable Name
    public const string cookies_RefreshToken = "refreshToken";
    #endregion

    #region ErrorAndSuccessMessages


    public const string Auth_InvalidToken = "Invalid token.";
    public const string Auth_NoToken = "No token found.";
    public const string Auth_TokenRevoked = "Token Revoked.";
    public const string Auth_InvalidUsernamePassword = "User name or password is incorrect.";
    public const string Auth_RevokeWithoutReplacement = "Revoked without replacement.";
    public const string Auth_RevokeWithReplacement = "Replaced by new token";
    public const string Auth_Atmpt_Reuse_OfRevokedAncestor_Token = "Attempted reuse of revoked ancestor token: {0}";
    public const string Auth_Atmpt_NoUserFoundWithUserCred = "It seems that you are not registered with us. Please click on SighUp button to create an account with us.";
    public const string Auth_Atmpt_NoSaltFoundWithUserCred = "Something went wrong. Kindly get in touch with hailgrotech.com for assistance.";
    public const string Auth_LoginSucess = "Login Successful. Welcome!";
    #endregion


    #region SignUp
    //Messages
    public const string SignUp_InvalidMobileNumber = "Invalid Mobile number.";
    public const string SignUp_UnableToGenOtp = "Unable to Generate OTP.";
    public const string SignUp_UnableToSendOtp = "Unable to Send OTP at this moment.";
    public const string SignUp_OtpGeneratedSucess = "OTP generated successfully.";
    public const string SignUp_OtpValidateSucess = "OTP validation successfully.";
    public const string SignUp_NoOTPFoundInDB = "Couldn't find any OTP for this mobile number.";
    public const string SignUp_InvalidOTP = "Invalid OTP.";
    public const string SignUp_OTPExpired = "OTP Expired.";
    public const string SignUp_OTPAlreadyUsed = "OTP is already used.";

    #endregion

    #region User Service

    public const string User_UserNotFound = "Unable to find user. ";
    public const string User_UserExistsWithMobileOrEmail = "User already exists for given mobile and email.";
    public const string User_FailedToCreateNewUser = "Failed to create new User.";
    public const string User_FailedToUpdateUser = "Failed to update User.";
    public const string User_UserCreated = "User created successfully.";
    public const string User_UserUpdated = "User updated successfully.";

    #endregion



    #region Enumerators
    public enum CountryCode
    {
        IN,
        UK
    }
    public enum Days
    {
        Sun,
        Mon,
        Tue
    }

    #endregion
}