using GptWeb.Services;
using Moq;
using NUnit.Framework;
using System.Net;

namespace GptWeb.Tests
{
    [TestFixture]
    public class FineTunedChatServiceTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<ILogger<FineTunedChatService>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<FineTunedChatService>>();

            // Mock configuration values as needed
            _mockConfiguration.SetupGet(x => x["FineTunedModelName"]).Returns("fine-tuned-model");
            _mockConfiguration.SetupGet(x => x["OPENAI_API_KEY"]).Returns("test_api_key");
        }

        // Test methods

        [Test]
        public async Task GetResponse_ReturnsSuccessfulResponse()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"choices\": [{\"text\": \"Test response\"}]}")
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var client = new HttpClient(fakeHttpMessageHandler);

            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new FineTunedChatService(_mockHttpClientFactory.Object, _mockConfiguration.Object, _mockLogger.Object);

            // Act
            var result = await service.GetResponse("Test prompt");

            // Assert
            Assert.AreEqual("Test response", result);
        }

        [Test]
        public void GetResponse_ThrowsHttpRequestExceptionForNonSuccessStatusCode()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "Bad Request",
                Content = new StringContent("") // Empty content, you might want to simulate error content
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var client = new HttpClient(fakeHttpMessageHandler);

            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new FineTunedChatService(_mockHttpClientFactory.Object, _mockConfiguration.Object, _mockLogger.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await service.GetResponse("Test prompt"));
            Assert.That(ex.Message, Is.EqualTo("Error BadRequest: Bad Request"));
        }

        [Test]
        public void GetResponse_ThrowsInvalidOperationExceptionForMissingConfiguration()
        {
            // Arrange
            _mockConfiguration.SetupGet(x => x["FineTunedModelName"]).Returns((string)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new FineTunedChatService(_mockHttpClientFactory.Object, _mockConfiguration.Object, _mockLogger.Object));
        }

    }
}
