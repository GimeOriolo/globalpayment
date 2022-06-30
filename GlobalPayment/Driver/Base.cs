using GlobalPayment.Tools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GlobalPayment.Driver
{
    public class Base
    {
        public TimeSpan PageLoadTime = TimeSpan.FromSeconds(60);
        public TimeSpan ImplicitWaitTime = TimeSpan.FromSeconds(30);
        public TimeSpan AsynchronousJavaScript = TimeSpan.FromSeconds(60);
        public static readonly string DownloadFolder = AppDomain.CurrentDomain.BaseDirectory + @"\Descargas\";

        /// <summary>
        /// Inicia chrome driver sin headless
        /// </summary>
        /// <param name="windowMode"></param>
        /// 
        public IWebDriver Driver(bool windowMode = false)
        {
            try
            {
                Chrome.DownloadDriver();
                ChromeOptions driverOptions = new();
                ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Chrome.DriverPath));

                driverOptions.AddArguments("--no-sandbox");
                driverOptions.AddArguments("disable-popup-blocking");
                driverOptions.AddArguments("window-size=1920x1080");
                driverOptions.AddArguments("ignore-certificate-errors");
                driverOptions.AddArguments("safebrowsing-disable-download-protection");
                if (!windowMode) driverOptions.AddArguments("headless", "disable-gpu");
                driverOptions.AddUserProfilePreference("download.default_directory", DownloadFolder);
                driverOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
                driverService.HideCommandPromptWindow = true;

                if (!Directory.Exists(DownloadFolder)) Directory.CreateDirectory(DownloadFolder);

                IWebDriver WebDriver = new ChromeDriver(driverService, driverOptions, TimeSpan.FromMinutes(2));

                WebDriver.Manage().Window.Maximize();
                WebDriver.Manage().Timeouts().ImplicitWait = ImplicitWaitTime;
                WebDriver.Manage().Timeouts().PageLoad = PageLoadTime;
                WebDriver.Manage().Timeouts().AsynchronousJavaScript = AsynchronousJavaScript;

                return WebDriver;
            }
            catch (Exception e)
            {
                OjLog.WriteLine(NLog.LogLevel.Fatal, e);
                throw;
            }
        }

    }
}
