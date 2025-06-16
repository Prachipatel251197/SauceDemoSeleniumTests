using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SauceDemoSeleniumTests.Tests
{
    internal class LoginUserWithCorrectCredentials
    {

        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
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

        [Test]
        public void LoginUserAndDeleteAccount()
        {
            // Step 2: Navigate to url
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify home page visible
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");

            // Step 4: Click 'Signup / Login'
            driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();

            // Step 5: Verify 'Login to your account' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Login to your account']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='Login to your account']")).Displayed);

            // Step 6: Enter correct email and password
            driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys("pra_test15@testmail.com");
            driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys("TestPassword123");

            // Step 7: Click login button
            driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();

            // Step 8: Verify 'Logged in as username' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Logged in as')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")).Displayed);

            // Step 9: Click 'Delete Account' button

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement deleteLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/delete_account']")));
            deleteLink.Click();


            // Step 10: Verify 'ACCOUNT DELETED!' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//b[text()='Account Deleted!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//b[text()='Account Deleted!']")).Displayed);

        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }



    }
}
