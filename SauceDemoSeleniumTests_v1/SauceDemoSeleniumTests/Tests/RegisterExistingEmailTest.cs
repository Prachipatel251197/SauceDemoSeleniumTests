using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoSeleniumTests.Tests
{
    internal class RegisterExistingEmailTest
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
        public void RegisterUserWithExistingEmail_ShouldShowError()
        {
            // Step 2: Navigate to url
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify home page visible
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");

            // Step 4: Click 'Signup / Login' button
            driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();

            // Step 5: Verify 'New User Signup!' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='New User Signup!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='New User Signup!']")).Displayed);

            // Step 6: Enter name and already registered email
            driver.FindElement(By.XPath("//input[@data-qa='signup-name']")).SendKeys("Prachi");
            driver.FindElement(By.XPath("//input[@data-qa='signup-email']")).SendKeys("prachi_test1234@testmail.com"); // Existing email

            // Step 7: Click 'Signup' button
            driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

            // Step 8: Verify error 'Email Address already exist!' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Email Address already exist!']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//p[text()='Email Address already exist!']")).Displayed);
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
