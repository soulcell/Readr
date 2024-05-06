using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Readr.API.Services
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string _phoneNumber;

        public TwilioSmsService(IConfiguration configuration)
        {
            string? accountSid = configuration["Api:Twilio:AccountSid"];
            string? authToken = configuration["Api:Twilio:AuthToken"];
            string? phoneNumber = configuration["Api:Twilio:PhoneNumber"];

            ArgumentException.ThrowIfNullOrWhiteSpace(accountSid);
            ArgumentException.ThrowIfNullOrWhiteSpace(authToken);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

            this._phoneNumber = phoneNumber;

            TwilioClient.Init(accountSid, authToken);
        }

        public async Task<bool> Send(string phone, string message)
        {
            if (!Regex.IsMatch(phone, "^\\+[1-9]\\d{1,14}$"))
            {
                throw new ArgumentException($"{phone} is not a valid E.164 phone number");
            }

            MessageResource msg = await MessageResource.CreateAsync(from: this._phoneNumber, to: phone, body: message);

            return (msg.Status != MessageResource.StatusEnum.Failed);
        }
    }
}
