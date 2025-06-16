using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SauceDemoSeleniumTests.StepDefinitions
{
    [Binding]
    public class ContactUsFormTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Given(@"I launch the browser")]
        public void GivenILaunchTheBrowser()
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

        [When(@"I navigate to '(.*)'")]
        public void WhenINavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        [Then(@"the home page should be visible")]
        public void ThenTheHomePageShouldBeVisible()
        {
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");
        }

        [When(@"I click on the Contact Us button")]
        public void WhenIClickOnTheContactUsButton()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'Contact us')]")).Click();
        }

        [Then(@"the GET IN TOUCH header should be visible")]
        public void ThenTheGETINTOUCHHeaderShouldBeVisible()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[contains(text(),'Get In Touch')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[contains(text(),'Get In Touch')]")).Displayed);
        }

        [When(@"I fill in the contact form with name '(.*)', email '(.*)', subject '(.*)', message '(.*)'")]
        public void WhenIFillInTheContactForm(string name, string email, string subject, string message)
        {
            driver.FindElement(By.Name("name")).SendKeys(name);
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("subject")).SendKeys(subject);
            driver.FindElement(By.Name("message")).SendKeys(message);
        }

        [When(@"I upload the file '(.*)'")]
        public void WhenIUploadTheFile(string relativePath)
        {
            string filePath = Path.GetFullPath(relativePath);
            driver.FindElement(By.Name("upload_file")).SendKeys(filePath);
        }

        [When(@"I click on the Submit button")]
        public void WhenIClickOnTheSubmitButton()
        {
            IWebElement submitButton = driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();
        }

        [When(@"I accept the alert popup")]
        public void WhenIAcceptTheAlertPopup()
        {
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }

        [Then(@"I should see the success message")]
        public void ThenIShouldSeeTheSuccessMessage()
        {
            IWebElement successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.status.alert.alert-success")));
            Assert.That(successMessage.Text.Contains("Success! Your details have been submitted successfully."));
        }

        [When(@"I click on the Home button")]
        public void WhenIClickOnTheHomeButton()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'Home')]")).Click();
        }

        [Then(@"I should be navigated to the home page")]
        public void ThenIShouldBeNavigatedToTheHomePage()
        {
            wait.Until(ExpectedConditions.TitleContains("Automation Exercise"));
            Assert.That(driver.Title.Contains("Automation Exercise"), "Did not navigate to home page");
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
