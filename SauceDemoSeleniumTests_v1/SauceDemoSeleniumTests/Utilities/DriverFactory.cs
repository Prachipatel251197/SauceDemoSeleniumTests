using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoSeleniumTests.Utilities
{
    public class DriverFactory
    {
        public static IWebDriver GetDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(options);
        }
    }
}
