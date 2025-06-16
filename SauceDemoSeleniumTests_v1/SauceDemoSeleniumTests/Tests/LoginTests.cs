//using NUnit.Framework;
//using OpenQA.Selenium;
//using SauceDemoSeleniumTests.Pages;

//namespace SauceDemoSeleniumTests.Tests
//{
//    public class LoginTests
//    {
//        private IWebDriver driver;

//        [SetUp]
//        public void Setup()
//        {
//            driver = Utilities.DriverFactory.GetDriver();
//            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
//        }

//        [Test]
//        public void ValidLoginTest()
//        {
//            var login = new LoginPage(driver);
//            login.Login("standard_user", "secret_sauce");

//            Assert.That(driver.Url.Contains("inventory"), Is.True);
//        }

//        [TearDown]
//        public void Cleanup()
//        {
//            if (driver != null)
//            {
//                driver.Quit();    // Closes browser and ends session
//                driver.Dispose(); // Releases system resources (recommended)
//            }
//        }
//    }
//}
