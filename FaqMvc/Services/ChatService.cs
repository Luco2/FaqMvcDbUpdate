using GptWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GptWeb.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string apiKey = _configuration["OPENAI_API_KEY"] ?? throw new InvalidOperationException("OpenAI API key not found.");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetResponse(string prompt)
        {
            var useMockOpenAi = _configuration["USE_MOCK_OPENAI"];
            if (bool.TryParse(useMockOpenAi, out bool shouldUseMock) && shouldUseMock)
            {
                return GetMockResponse(prompt);
            }

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    model = "text-davinci-002",
                    prompt = prompt,
                    temperature = 1,
                    max_tokens = 100
                }),
                Encoding.UTF8, "application/json"
            );

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error {response.StatusCode}: {response.ReasonPhrase}");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ChatResponse>(responseString);
                if (responseData?.Choices?.Count > 0)
                {
                    return responseData.Choices[0].Text?.Trim();
                }

                throw new InvalidOperationException("No response text found from OpenAI.");
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                throw new InvalidOperationException("An error occurred while fetching response from OpenAI.", ex);
            }
        }

        private string GetMockResponse(string prompt)
        {
            return "Mocked: " + prompt;
        }
    }

}
