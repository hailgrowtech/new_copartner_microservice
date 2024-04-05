using PhoneNumbers;

namespace CommonLibrary
{
    public static class MobileHelper
    {
        public static bool IsMobileNumberValid(string mobile, string countryCode)
        {
            PhoneNumber phoneNumber = new PhoneNumber();
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                phoneNumber = phoneUtil.Parse(mobile, countryCode.ToString());
            }
            catch (NumberParseException)
            {
                //logError here.
                return false;
            }
            return phoneUtil.IsValidNumber(phoneNumber);
        }

        public static PhoneNumberType IsNumberTypeMobile(string number, string countryCode)
        {
            PhoneNumber phoneNumber = new PhoneNumber();
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                phoneNumber = phoneUtil.Parse(number, countryCode);
            }
            catch (NumberParseException)
            {
                //logError here.
                // return null;
            }
            return phoneUtil.GetNumberType(phoneNumber);
        }

    }
}
