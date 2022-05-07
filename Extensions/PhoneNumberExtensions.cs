using System.Linq;

namespace AutoPartsApp
{
    public static class PhoneNumberExtensions
    {
        public static string FromPhoneNumberToDigits(this string phoneNumber)
        {
            return string.Join("",
                               phoneNumber
                                .ToCharArray()
                                .Where(c =>
                                {
                                    return char.IsDigit(c);
                                }));
        }
    }
}
