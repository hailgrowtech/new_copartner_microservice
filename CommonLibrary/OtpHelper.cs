namespace CommonLibrary
{
    public static class OtpHelper
    {
        public static int GenerateOTP4Digit()
        {
            Random generator = new Random();
            return generator.Next(100, 9999);
        }
        public static int GenerateOTP6Digit()
        {
            Random generator = new Random();
            return generator.Next(1000, 669999);
        }
    }
}
