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
    internal class ContactUsFormTest
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
        public void ContactUsForm_Submission_Success()
        {
            // Step 2: Navigate to url
            driver.Navigate().GoToUrl("http://automationexercise.com");

            // Step 3: Verify home page visible
            Assert.That(driver.Title.Contains("Automation Exercise"), "Home page not loaded");

            // Step 4: Click on 'Contact Us' button
            driver.FindElement(By.XPath("//a[contains(text(),'Contact us')]")).Click();

            // Step 5: Verify 'GET IN TOUCH' is visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[contains(text(),'Get In Touch')]")));
            Assert.IsTrue(driver.FindElement(By.XPath("//h2[contains(text(),'Get In Touch')]")).Displayed);

            // Step 6: Enter name, email, subject, message
            driver.FindElement(By.Name("name")).SendKeys("Prachi Patel");
            driver.FindElement(By.Name("email")).SendKeys("prachi_test15@testmail.com");
            driver.FindElement(By.Name("subject")).SendKeys("Test Subject");
            driver.FindElement(By.Name("message")).SendKeys("This is a test message from Selenium automation.");

            // Step 7: Upload file
            // Provide path to a file you have on your system; make sure this path exists on your machine
            string filePath = Path.GetFullPath("C:\\prachi.patel\\hello.js"); // Replace with your actual test file path
            driver.FindElement(By.Name("upload_file")).SendKeys(filePath);

            // Step 8: Click 'Submit' button
            IWebElement submitButton = driver.FindElement(By.CssSelector("input[data-qa='submit-button']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();

            // Step 9: Handle alert popup: Click OK button
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();

            // Step 10: Verify success message
            var wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Wait until the success message div with all classes is visible
            IWebElement successMessage = wait1.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector("div.status.alert.alert-success")
                )
            );

            // Verify success message text
            Assert.That(successMessage.Text.Contains("Success! Your details have been submitted successfully."));

            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'status alert-success')]")));
            //string successMessage = driver.FindElement(By.XPath("//div[contains(@class,'status alert-success')]")).Text;
            //Assert.That(successMessage.Contains("Success! Your details have been submitted successfully."), "Success message not found");

            // Step 11: Click 'Home' button and verify landed on home page
            driver.FindElement(By.XPath("//a[contains(text(),'Home')]")).Click();

            wait.Until(ExpectedConditions.TitleContains("Automation Exercise"));
            Assert.That(driver.Title.Contains("Automation Exercise"), "Did not navigate to home page");
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
