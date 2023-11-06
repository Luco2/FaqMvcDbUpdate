using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GptWeb.Tests.Selenium
{
    public class AccountSelenium
    {
        [TestFixture]
        public class AccountControllerSeleniumTests
        {
            private IWebDriver _driver;

            [SetUp]
            public void SetUp()
            {
                // Initialize the ChromeDriver. Make sure chromedriver is in your PATH or specify the location.
                _driver = new ChromeDriver();
            }

            [Test]
            public async Task Login_ValidUser_RedirectsToHomePage()
            {
                // Navigate to the login page
                _driver.Navigate().GoToUrl("https://localhost:7296/Identity/Account/Login"); // Update with the correct URL for your application

                // Find the email and password fields
                var emailField = _driver.FindElement(By.Name("Email"));
                var passwordField = _driver.FindElement(By.Name("Password"));

                // Type the user's email and password
                emailField.SendKeys("user@example.com");
                passwordField.SendKeys("password");

                // Find and click the login button
                var loginButton = _driver.FindElement(By.XPath("//button[contains(text(),'Log In')]"));
                loginButton.Click();

                // Wait for redirect to complete
                await Task.Delay(1000); // Replace with explicit wait

                // Check that we have been redirected to the home page
                Assert.That(_driver.Url, Is.EqualTo("http://localhost:5000/")); // Update with the correct URL
            }

            [TearDown]
            public void TearDown()
            {
                // Close the browser and dispose of the driver
                _driver.Quit();
            }
        }
    }
}
