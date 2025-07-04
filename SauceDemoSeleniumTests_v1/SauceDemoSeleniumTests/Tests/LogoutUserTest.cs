﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoSeleniumTests.Tests
{
    internal class LogoutUserTest
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
        public void LogoutUserTest_SuccessfulLogout()
        {
            // Step 2: Navigate to url
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify home page visible
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");

            // Step 4: Click 'Signup / Login' button
            driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();

            // Step 5: Verify 'Login to your account' visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Login to your account']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='Login to your account']")).Displayed);

            // Step 6: Enter correct email and password
            driver.FindElement(By.XPath("//input[@data-qa='login-email']")).SendKeys("prachi_test1234@testmail.com");
            driver.FindElement(By.XPath("//input[@data-qa='login-password']")).SendKeys("TestPassword123");

            // Step 7: Click 'login' button
            driver.FindElement(By.XPath("//button[@data-qa='login-button']")).Click();

            // Step 8: Verify 'Logged in as username' is visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Logged in as')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//a[contains(text(),'Logged in as')]")).Displayed);

            // Step 9: Click 'Logout' button
            var logoutBtn = driver.FindElement(By.XPath("//a[contains(text(),'Logout')]"));
            logoutBtn.Click();

            // Step 10: Verify user is navigated to login page
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[text()='Login to your account']")));
            Assert.IsTrue(driver.Url.Contains("login"));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[text()='Login to your account']")).Displayed);
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
