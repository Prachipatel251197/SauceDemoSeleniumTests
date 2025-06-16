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

    public class LoginInvalidUserTest
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
        public void LoginUserWithIncorrectCredentials_ShowsError()
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

            // Step 6: Enter incorrect email and password
            driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys("wrong_email@example.com");
            driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys("wrong_password");

            // Step 7: Click login button
            driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();

            // Step 8: Verify error message visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]")));
            var errorMessage = driver.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]")).Text;

            Assert.AreEqual("Your email or password is incorrect!", errorMessage);
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }

    }
}
