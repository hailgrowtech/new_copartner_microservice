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


    #region SignIn
    //Messages
    public const string SignIn_InvalidMobileNumber = "Invalid Mobile number.";
    public const string SignIn_UnableToGenOtp = "Unable to Generate OTP.";
    public const string SignIn_UnableToSendOtp = "Unable to Send OTP at this moment.";
    public const string SignIn_OtpGeneratedSucess = "OTP generated successfully.";
    public const string SignIn_OtpValidateSucess = "OTP validation successfully.";
    public const string SignIn_NoOTPFoundInDB = "Couldn't find any OTP for this mobile number.";
    public const string SignIn_InvalidOTP = "Invalid OTP.";
    public const string SignIn_OTPExpired = "OTP Expired.";
    public const string SignIn_OTPAlreadyUsed = "OTP is already used.";

    #endregion

    #region User Service

    public const string User_UserNotFound = "Unable to find user. ";
    public const string User_UserExistsWithMobileOrEmail = "User already exists for given mobile and email.";
    public const string User_FailedToCreateNewUser = "Failed to create new User.";
    public const string User_FailedToUpdateUser = "Failed to update User.";
    public const string User_UserCreated = "User created successfully.";
    public const string User_UserUpdated = "User updated successfully.";

    #endregion

    #region Experts Service

    public const string Expert_ExpertNotFound = "Unable to find experts. ";
    public const string Expert_ExpertExistsWithMobileOrEmail = "Experts already exists for given mobile and email.";
    public const string AffiliatePartner_FailedToCreateAffiliatePartner = "Failed to create new experts.";
    public const string Expert_FailedToUpdateExpert = "Failed to update Expert.";
    public const string Expert_ExpertCreated = "Expert created successfully.";
    public const string Expert_ExpertUpdated = "Expert updated successfully.";

    #endregion

    #region Course Service

    public const string Course_CourseNotFound = "Unable to find courses. ";
    public const string Course_FailedToCreateNewCourse = "Failed to create new course.";
    public const string Course_FailedToUpdateCourse = "Failed to update Course.";
    public const string Course_CourseCreated = "Course created successfully.";
    public const string Course_CourseUpdated = "Course updated successfully.";

    #endregion

    #region Affiliate Partner Service

    public const string AffiliatePartner_AffiliatePartnerNotFound = "Unable to find Affiliate Partners. ";
    public const string AffiliatePartner_AffiliatePartnerExistsWithMobileOrEmail = "Affiliate Partner already exists for given mobile and email.";
    public const string AffiliatePartner_FailedToCreateNewAffiliatePartner = "Failed to create new Affiliate Partner.";
    public const string AffiliatePartner_FailedToUpdateAffiliatePartner = "Failed to update Affiliate Partner.";
    public const string AffiliatePartner_AffiliatePartnerCreated = "Affiliate Partner created successfully.";
    public const string AffiliatePartner_AffiliatePartnerUpdated = "Affiliate Partner updated successfully.";
    #endregion


    #region Subscriber Service

    public const string Subscriber_SubscriberNotFound = "Unable to find Subscribers. ";
    public const string Subscriber_FailedToCreateNewSubscriber = "Failed to create new Subscriber.";
    public const string Subscriber_FailedToUpdateSubscriber = "Failed to update Subscriber.";
    public const string Subscriber_SubscriberCreated = "Subscriber created successfully.";
    public const string Subscriber_SubscriberUpdated = "Subscriber updated successfully.";

    #endregion

    #region Subscription Service

    public const string Subscription_SubscriberNotFound = "Unable to find Subscription. ";
    public const string Subscription_FailedToCreateNewSubscription = "Failed to create new Subscription.";
    public const string Subscription_FailedToUpdateSubscription = "Failed to update Subscription.";
    public const string Subscription_SubscriptionCreated = "Subscription created successfully.";
    public const string Subscription_SubscriptionUpdated = "Subscription updated successfully.";

    #endregion


    #region Common Messages 
    public const string Common_NoRecordFound = "No record founds. ";
    public const string Common_RecordDeleted = "Record's deleted successfully. ";
    public const string Common_RecordCreated = "Record's created successfully.";
    public const string Common_FailedToCreateNewRecord = "Failed to create new record's.";
    public const string Common_AlreadyExistsRecord = "Record's already exists.";
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