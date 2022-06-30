using OpenQA.Selenium;

namespace GlobalPayment.Pages
{
    internal class Home
    {
        protected IWebDriver driver;
      
        protected IWebElement BtnSignin => driver.FindElement(By.Id("signin_button"));


    }
}
