namespace Readr.API.Services
{
    public interface IAuthService
    {
        string GetSecurityCode(string phone);

        bool CheckSecurityCode(string phone, string code);
    }
}
