using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class RefarralCode
    {
        public static string GenerateReferralCode(string partnerName)
        {
            // Ensure the partner name is at least 3 characters long
            if (partnerName.Length < 3)
            {
                throw new ArgumentException("Partner name must be at least 3 characters long.");
            }

            // Take the first three letters of the partner name
            string namePrefix = partnerName.Substring(0, 3).ToUpper();

            // Generate three random numbers
            Random random = new Random();
            string randomNumbers = string.Concat(Enumerable.Range(0, 3).Select(_ => random.Next(0, 10).ToString()));

            // Combine the name prefix and random numbers to form the referral code
            string referralCode = $"{namePrefix}{randomNumbers}";

            return referralCode;
        }
    }

}
