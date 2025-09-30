using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DotNetEnv;
namespace SmartStudentQueryAPI.Services
{
    public class AIHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public AIHelper(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Load environment variables from .env file
            DotNetEnv.Env.Load();

            // Get the API key from the environment variable
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") 
                      ?? throw new ArgumentNullException("OpenAI API key not set in environment variables");

            // Get the model from appsettings.json
            _model = configuration["OpenAI:Model"] ?? "gpt-3.5-turbo";
        }

        public async Task<string> GetAnswerAsync(string userPrompt)
        {
            if (string.IsNullOrWhiteSpace(userPrompt))
                return string.Empty;

            var payload = new
            {
                model = _model,
                messages = new object[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = userPrompt }
                },
                max_tokens = 150,
                temperature = 0.2
            };

            var json = JsonSerializer.Serialize(payload);
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            using var resp = await _httpClient.SendAsync(request);
            var respText = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                // surface error text (useful during dev)
                throw new Exception($"OpenAI API error: {resp.StatusCode} - {respText}");
            }

            using var doc = JsonDocument.Parse(respText);
            // path: choices[0].message.content
            if (doc.RootElement.TryGetProperty("choices", out var choices)
                && choices.GetArrayLength() > 0
                && choices[0].TryGetProperty("message", out var message)
                && message.TryGetProperty("content", out var content))
            {
                return content.GetString()?.Trim() ?? string.Empty;
            }

            return string.Empty;
        }
    }
}
