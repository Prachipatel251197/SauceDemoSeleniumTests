
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationExerciseTests
{
    public class RegisterUserTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            var options = new ChromeOptions();

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.autofill_enabled", false);
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.address_autofill_enabled", false);
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://automationexercise.com");
        }

        [Test]
        public void RegisterUser()

        {
            Assert.That(driver.Title.Contains("Automation Exercise"));

            driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();
            Assert.That(driver.FindElement(By.XPath("//h2[text()='New User Signup!']")).Displayed);

            driver.FindElement(By.XPath("//input[@data-qa='signup-name']")).SendKeys("Prachi");
            driver.FindElement(By.XPath("//input[@data-qa='signup-email']")).SendKeys("pra_test15@testmail.com");
            driver.FindElement(By.XPath("//button[@data-qa='signup-button']")).Click();

            Assert.That(driver.FindElement(By.XPath("//b[text()='Enter Account Information']")).Displayed);

            driver.FindElement(By.Id("id_gender2")).Click(); // Title: Mrs.
            driver.FindElement(By.Id("password")).SendKeys("TestPassword123");
            driver.FindElement(By.Id("days")).SendKeys("15");
            driver.FindElement(By.Id("months")).SendKeys("June");
            driver.FindElement(By.Id("years")).SendKeys("1997");

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.Id("newsletter")));
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.Id("optin")));

            driver.FindElement(By.Id("first_name")).SendKeys("Prachi");
            driver.FindElement(By.Id("last_name")).SendKeys("Patel");
            driver.FindElement(By.Id("company")).SendKeys("TestCompany");
            driver.FindElement(By.Id("address1")).SendKeys("123 Test Street");
            driver.FindElement(By.Id("address2")).SendKeys("Suite 456");
            driver.FindElement(By.Id("country")).SendKeys("United States");
            driver.FindElement(By.Id("state")).SendKeys("Alabama");
            driver.FindElement(By.Id("city")).SendKeys("Montgomery");
            driver.FindElement(By.Id("zipcode")).SendKeys("36117");
            driver.FindElement(By.Id("mobile_number")).SendKeys("1234567890");

            IJavaScriptExecutor js1 = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement createAccountBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='create-account']")));
            js1.ExecuteScript("arguments[0].click();", createAccountBtn);
            Assert.That(driver.FindElement(By.XPath("//b[text()='Account Created!']")).Displayed);

            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("//a[text()='Continue']")));

            Assert.That(driver.PageSource.Contains("Logged in as"));
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement deleteLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/delete_account']")));
            deleteLink.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//b[text()='Account Deleted!']")));

        }

        [TearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
