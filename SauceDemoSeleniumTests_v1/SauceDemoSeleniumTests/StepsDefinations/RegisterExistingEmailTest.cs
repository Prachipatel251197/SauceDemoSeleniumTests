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
    public class RegisterExistingEmailTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Given(@"Launch browser")]
        public void GivenLaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.autofill_enabled", false);
            options.AddUserProfilePreference("profile.address_autofill_enabled", false);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [When(@"Navigate to url '(.*)'")]
        public void WhenNavigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"Verify that home page is visible successfully")]
        public void ThenVerifyThatHomePageIsVisibleSuccessfully()
        {
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");
        }

        [When(@"Click on 'Signup / Login' button")]
        public void WhenClickOnSignupLoginButton()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();
        }

        [Then(@"Verify 'New User Signup!' is visible")]
        public void ThenVerifyNewUserSignupIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='New User Signup!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='New User Signup!']")).Displayed);
        }

        [When(@"Enter name '(.*)' and already registered email '(.*)'")]
        public void WhenEnterNameAndAlreadyRegisteredEmail(string name, string email)
        {
            driver.FindElement(By.XPath("//input[@data-qa='signup-name']")).SendKeys(name);
            driver.FindElement(By.XPath("//input[@data-qa='signup-email']")).SendKeys(email);
        }

        [When(@"Click 'Signup' button")]
        public void WhenClickSignupButton()
        {
            driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();
        }

        [Then(@"Verify error 'Email Address already exist!' is visible")]
        public void ThenVerifyErrorEmailAddressAlreadyExistIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Email Address already exist!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//p[text()='Email Address already exist!']")).Displayed);
        }

        [AfterScenario]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
