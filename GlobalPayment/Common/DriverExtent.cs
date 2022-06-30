using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GlobalPayment.Tools
{
    public static class DriverExtent
    {
        public static void VerifyTitle(this IWebDriver driver, By by, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                string tit = driver.FindElement(by).Text;
                if (!tit.ToLower().Contains(title.ToLower()))
                {
                    throw new Exception("No se ingresó a la pantalla " + title);
                }
            }
        }

        public static void WaitElement(this IWebDriver driver, By by, double time)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(time));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(drv => drv.FindElement(by));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(3));
        }
    }
}
