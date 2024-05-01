namespace Readr.API.Services
{
    public class MockSmsService : ISmsService
    {
        private readonly ILogger<MockSmsService> logger;

        public MockSmsService(ILogger<MockSmsService> logger)
        {
            this.logger = logger;
        }

        public async Task<bool> Send(string phone, string message)
        {
            logger.Log(LogLevel.Information, "Mocking SMS to {1}, body: \"{2}\"", phone, message);
            return await Task.FromResult(true);
        }
    }
}
