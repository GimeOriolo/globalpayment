using OpenQA.Selenium;

namespace GlobalPayment.Pages
{
    internal class HomeFunctions : Home
    {
        #region Builder
        public HomeFunctions(IWebDriver iDriver)
        {
            driver = iDriver;
        }
        #endregion

        #region Methods
        public HomeFunctions ClickOnSignIn()
        {
            BtnSignin.Click();
            return this;
        }       
        #endregion
    }
}
