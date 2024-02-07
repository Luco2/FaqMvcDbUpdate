using FaqMvc.Data;
using GptWeb.Models;
using GptWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GptWeb.Services
{
    public class MockChatService : IChatService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MockChatService> _logger;

        public MockChatService(IConfiguration configuration, ILogger<MockChatService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GetResponse(string prompt)
        {
            var useMockOpenAi = _configuration.GetValue<bool>("USE_MOCK_OPENAI", false);

            if (useMockOpenAi)
            {
                _logger.LogInformation("Using mock OpenAI response.");
                return await Task.FromResult(GetMockResponse(prompt));
            }
            else
            {
                _logger.LogInformation("Not using mock. Attempted to call the real OpenAI API.");
                throw new InvalidOperationException("The mock service should not call the real OpenAI API.");
            }
        }

        private string GetMockResponse(string prompt)
        {
            if (prompt.Contains("fee for"))
            {
                return "The fee for [service] is $XX.XX.";
            }
            else
            {
                return "Mocked response for an open-ended question related to: " + prompt;
            }
        }

    }
}
