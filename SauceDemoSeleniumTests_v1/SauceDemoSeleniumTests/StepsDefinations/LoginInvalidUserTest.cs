using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace SauceDemoSeleniumTests.StepDefinitions
{
    [Binding]
    public class LoginInvalidUserSteps
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
    }
}
