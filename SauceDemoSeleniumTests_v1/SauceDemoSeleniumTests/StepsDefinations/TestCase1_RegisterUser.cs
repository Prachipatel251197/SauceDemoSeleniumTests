using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace AutomationExerciseTests.StepDefinitions
{
    [Binding]
    public class TestCase1_RegisterUser
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Given(@"I launch the browser and navigate to ""(.*)""")]
        public void GivenILaunchTheBrowserAndNavigateTo(string url)
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"I verify that the home page is visible successfully")]
        public void ThenIVerifyThatTheHomePageIsVisibleSuccessfully()
        {
            Assert.IsTrue(driver.Title.Contains("Automation Exercise"), "Home page title does not match");
        }

        [When(@"I click on ""(.*)"" button")]
        public void WhenIClickOnButton(string buttonText)
        {
            var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//a[contains(text(),'{buttonText}')]")));
            button.Click();
        }

        [Then(@"I verify ""(.*)"" is visible")]
        public void ThenIVerifyIsVisible(string expectedText)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[contains(text(),'{expectedText}')]")));
            Assert.IsTrue(element.Displayed, $"Element with text '{expectedText}' not visible");
        }

        [When(@"I enter name ""(.*)"" and email ""(.*)""")]
        public void WhenIEnterNameAndEmail(string name, string email)
        {
            var nameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@data-qa='signup-name']")));
            var emailInput = driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));

            nameInput.Clear();
            nameInput.SendKeys(name);
            emailInput.Clear();
            emailInput.SendKeys(email);
        }

        [When(@"I click ""(.*)"" button")]
        public void WhenIClickButton(string buttonText)
        {
            var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//button[contains(text(),'{buttonText}')]")));
            button.Click();
        }

        [When(@"I fill the signup form with valid details")]
        public void WhenIFillTheSignupFormWithValidDetails()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("id_gender2"))).Click(); // Title: Mrs.

            driver.FindElement(By.Id("password")).SendKeys("TestPassword123");

            // Use SelectElement for dropdowns
            new SelectElement(driver.FindElement(By.Id("days"))).SelectByValue("15");
            new SelectElement(driver.FindElement(By.Id("months"))).SelectByText("June");
            new SelectElement(driver.FindElement(By.Id("years"))).SelectByValue("1997");

            // Check checkboxes with JS click
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.Id("newsletter")));
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.Id("optin")));

            driver.FindElement(By.Id("first_name")).SendKeys("Prachi");
            driver.FindElement(By.Id("last_name")).SendKeys("Patel");
            driver.FindElement(By.Id("company")).SendKeys("TestCompany");
            driver.FindElement(By.Id("address1")).SendKeys("123 Test Street");
            driver.FindElement(By.Id("address2")).SendKeys("Suite 456");
            new SelectElement(driver.FindElement(By.Id("country"))).SelectByText("United States");
            driver.FindElement(By.Id("state")).SendKeys("Alabama");
            driver.FindElement(By.Id("city")).SendKeys("Montgomery");
            driver.FindElement(By.Id("zipcode")).SendKeys("36117");
            driver.FindElement(By.Id("mobile_number")).SendKeys("1234567890");
        }

        [When(@"I click ""Create Account"" button")]
        public void WhenIClickCreateAccountButton()
        {
            var createAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='create-account']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", createAccountBtn);
        }

        [Then(@"I verify that ""(.*)"" is visible")]
        public void ThenIVerifyThatIsVisible(string expectedText)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//b[contains(text(),'{expectedText}')]")));
            Assert.IsTrue(element.Displayed, $"Element with text '{expectedText}' not visible");
        }

        [When(@"I click ""Continue"" button")]
        public void WhenIClickContinueButton()
        {
            var continueBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Continue')]")));
            continueBtn.Click();
        }

        [Then(@"I verify that ""Logged in as (.*)"" is visible")]
        public void ThenIVerifyThatLoggedInAsIsVisible(string username)
        {
            var loggedInText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//a[contains(text(),'Logged in as {username}')]")));
            Assert.IsTrue(loggedInText.Displayed, $"Logged in as '{username}' not visible");
        }

        [When(@"I click ""Delete Account"" button")]
        public void WhenIClickDeleteAccountButton()
        {
            var deleteAccountBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/delete_account']")));
            deleteAccountBtn.Click();
        }

        [Then(@"I verify that ""ACCOUNT DELETED!"" is visible and click ""Continue"" button")]
        public void ThenIVerifyThatAccountDeletedIsVisibleAndClickContinueButton()
        {
            var accountDeleted = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//b[text()='Account Deleted!']")));
            Assert.IsTrue(accountDeleted.Displayed, "'ACCOUNT DELETED!' message not visible");

            var continueBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Continue')]")));
            continueBtn.Click();
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
