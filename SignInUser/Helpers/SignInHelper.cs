using System.Text.RegularExpressions;

namespace SignInService.Helpers;

public static class SignInHelper
{
    public static bool IsMobileNumberValid(string number)
    {
        //use aws or azure service to validate mobile number. Do something like textMessaginService. It will be validated based on country.
        return Regex.Match(number.Trim(), @"^(\+[0-9]{12})$").Success;
    } 
    public static bool ValidateOTP(string mobileNumber, string OtpToValidate)
    {
            
        //return ture if opt is successfully validated or else return false. 
        return true;// if opt is validated successfully ;
    }
    // Validate mobile number using external services like Amazon or Azure.
 

}
