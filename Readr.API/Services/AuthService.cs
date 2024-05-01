using System.Security.Cryptography;

namespace Readr.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly Dictionary<string, string> codeDictionary = new Dictionary<string, string>();

        public AuthService()
        {
            
        }

        public bool CheckSecurityCode(string phone, string code)
        {
            return codeDictionary.ContainsKey(phone) && codeDictionary[phone] == code;
        }

        public string GetSecurityCode(string phone)
        {
            string code = GenerateSecurityCode();

#if DEBUG
            if (phone == "123456") code = "123456";
#endif

            if (codeDictionary.ContainsKey(phone))
            {
                codeDictionary[phone] = code;
            }
            else
            {
                codeDictionary.Add(phone, code);
            }

            return code;
        }

        private static string GenerateSecurityCode()
        {
            return RandomNumberGenerator.GetInt32(1000000).ToString("D6");
        }
    }
}
