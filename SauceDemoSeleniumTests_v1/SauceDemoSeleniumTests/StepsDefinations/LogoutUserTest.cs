using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SauceDemoSeleniumTests.StepDefinitions
{
    [Binding]
    public class LogoutSteps
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Given(@"I launch the browser and navigate to ""(.*)""")]
        public void GivenILaunchTheBrowserAndNavigateTo(string url)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.autofill_enabled", false);
            options.AddUserProfilePreference("profile.address_autofill_enabled", false);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"I verify that home page is visible successfully")]
        public void ThenIVerifyThatHomePageIsVisibleSuccessfully()
        {
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");
        }

        [When(@"I click on ""(.*)"" button")]
        public void WhenIClickOnButton(string buttonName)
        {
            switch (buttonName.ToLower())
            {
                case "signup / login":
                    driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();
                    break;
                case "login":
                    driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();
                    break;
                case "logout":
                    driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();
                    break;
                default:
                    throw new NotImplementedException($"Button '{buttonName}' not implemented.");
            }
        }

        [Then(@"I verify ""(.*)"" is visible")]
        public void ThenIVerifyIsVisible(string text)
        {
            var xpath = $"//h2[text()='{text}']";
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            Assert.IsTrue(driver.FindElement(By.XPath(xpath)).Displayed);
        }

        [When(@"I enter email ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterEmailAndPassword(string email, string password)
        {
            driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys(password);
        }

        [Then(@"I verify ""(.*)"" username is visible")]
        public void ThenIVerifyUsernameIsVisible(string partialText)
        {
            var xpath = $"//a[contains(text(),'{partialText}')]";
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            Assert.IsTrue(driver.FindElement(By.XPath(xpath)).Displayed);
        }

        [Then(@"I verify user is navigated to login page")]
        public void ThenIVerifyUserIsNavigatedToLoginPage()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Login to your account']")));
            Assert.IsTrue(driver.Url.Contains("login"));
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
