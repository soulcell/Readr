using Readr.API.Models;
using System.Text.Json.Nodes;

namespace Readr.API.Services
{
    /// <summary>
    /// Book cover service using book cover API by w3slley
    /// <see cref="https://github.com/w3slley/bookcover-api"/>
    /// </summary>
    public class WeslleyBookCoverService : IBookCoverService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly ILogger<WeslleyBookCoverService> logger;

        public WeslleyBookCoverService(IConfiguration configuration, ILogger<WeslleyBookCoverService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<string?> FindBookCoverUrlAsync(BookTitle bookTitle)
        {
            string? uriString = configuration["Api:Weslley:Uri"];

            if (string.IsNullOrWhiteSpace(uriString))
            {
                return null;
            }

            string? json = string.Empty;
            try
            {
                json = await this.httpClient.GetStringAsync($"{uriString}?book_title={bookTitle.Title}&author_name={bookTitle.Author}");
            }
            catch (HttpRequestException ex)
            {
                this.logger.LogError("Weslley API request failed: {err}", ex.StatusCode);
            }

            JsonNode? jsonNode = JsonNode.Parse(json);
            if (jsonNode is null || jsonNode["url"] is null)
            {
                return null;
            }

            return jsonNode["url"]!.ToString();
        }
    }
}
