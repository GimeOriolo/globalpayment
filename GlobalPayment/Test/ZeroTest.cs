using GlobalPayment.Pages;
using GlobalPayment.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GlobalPayment.Test
{
    [TestClass]
    public class ZeroTest
    {   

        //Instancia del driver
        readonly IWebDriver driver = new Driver.Base().Driver(true);        

        [TestMethod]
        public void GoToSign()
        {
            try
            {  
                //Navegacion de la pagina
                driver.Navigate().GoToUrl("http://zero.webappsecurity.com/index.html");

                //Instancia de clase de pagina Home
                HomeFunctions home = new(driver);

                //LLamada al metodo clickonsigin
                home.ClickOnSignIn();

                //Espera a que aparezca el elemento
                driver.WaitElement(By.Id("user_login"),4);

                //Verifica el titulo de la pagina
                driver.VerifyTitle(By.TagName("h3"), "Log in to ZeroBank");


            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestCleanup]
        public void End()
        {
            driver.Quit();
        }
    }
}
