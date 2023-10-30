using Newtonsoft.Json;
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
        }

        public async Task<string> GetResponse(string prompt)
        {
            // Check if we should use the mock based on configuration
            var useMockOpenAi = _configuration["USE_MOCK_OPENAI"];
            if (bool.TryParse(useMockOpenAi, out bool shouldUseMock) && shouldUseMock)
            {
                return GetMockResponse(prompt);
            }

            // Use the real API
            string apiKey = _configuration["OPENAI_API_KEY"] ?? throw new InvalidOperationException("OpenAI API key not found.");
            _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);

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

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Extract the response from the returned JSON
            dynamic responseData = JsonConvert.DeserializeObject(responseString);
            string result = responseData.choices[0].text;

            return result.Trim(); // Return the answer
        }

        private string GetMockResponse(string prompt)
        {
            // Return a mocked response based on the prompt.      
            return "Mocked: " + prompt;
        }
    }
}
