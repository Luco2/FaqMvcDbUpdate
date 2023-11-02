using GptWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GptWeb.Services
{
    public class FineTunedChatService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _fineTunedModelName;

        public FineTunedChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;

            _fineTunedModelName = _configuration["FineTunedModelName"] ?? throw new InvalidOperationException("Fine-tuned model name not configured.");

            InitializeClient();
        }

        private void InitializeClient()
        {
            string apiKey = _configuration["OPENAI_API_KEY"] ?? throw new InvalidOperationException("OpenAI API key not found.");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetResponse(string prompt)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    model = _fineTunedModelName,
                    prompt = prompt,
                    temperature = 0.7, // You might want to fine-tune this parameter as well
                    max_tokens = 150 // Adjust according to your need
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
                // Consider logging the exception here
                throw new InvalidOperationException("An error occurred while fetching response from the fine-tuned OpenAI model.", ex);
            }
        }
    }
}
