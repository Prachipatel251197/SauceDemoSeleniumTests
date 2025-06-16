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
    public class LoginUserWithCorrectCredentials
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

        [Then(@"Verify 'Login to your account' is visible")]
        public void ThenVerifyLoginToYourAccountIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Login to your account']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='Login to your account']")).Displayed);
        }

        [When(@"Enter correct email address '(.*)' and password '(.*)'")]
        public void WhenEnterCorrectEmailAddressAndPassword(string email, string password)
        {
            driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys(password);
        }

        [When(@"Click 'login' button")]
        public void WhenClickLoginButton()
        {
            driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();
        }

        [Then(@"Verify that 'Logged in as username' is visible")]
        public void ThenVerifyThatLoggedInAsUsernameIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Logged in as')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")).Displayed);
        }

        [When(@"Click 'Delete Account' button")]
        public void WhenClickDeleteAccountButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement deleteLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/delete_account']")));
            deleteLink.Click();
        }

        [Then(@"Verify that 'ACCOUNT DELETED!' is visible")]
        public void ThenVerifyThatAccountDeletedIsVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//b[text()='Account Deleted!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//b[text()='Account Deleted!']")).Displayed);
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
