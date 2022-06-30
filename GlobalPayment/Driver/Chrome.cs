using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace GlobalPayment.Driver
{
    internal class Chrome
    {
        public static readonly string DriverPath = string.Format(@"C:\Webdriver\{0}\chromedriver.exe", GetChromeVersion());
        private static readonly string DriverPathZip = string.Format(@"C:\Webdriver\{0}\chromedriver.zip", GetChromeVersion());

        /// <summary>
        /// Descarga y descomprime la version del driver
        /// </summary>
        /// <param name="processName">Nombre del proceso</param>
        public static void DownloadDriver()
        {
            try
            {
                if (!File.Exists(DriverPath))
                {
                    using var client = new WebClient();
                    string URL = string.Format("https://chromedriver.storage.googleapis.com/{0}/chromedriver_win32.zip", GetLastReleaseVersion());

                    var buffer = client.DownloadData(URL);

                    if (!Directory.Exists(Path.GetDirectoryName(DriverPath))) Directory.CreateDirectory(Path.GetDirectoryName(DriverPath));
                    File.WriteAllBytes(string.Format(DriverPathZip, GetChromeVersion()), buffer);

                    ZipFile.ExtractToDirectory(DriverPathZip, Path.GetDirectoryName(DriverPath));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la version de Chrome
        /// </summary>
        /// <param name="processName">Nombre del proceso</param>
        private static string GetChromeVersion()
        {
            var pathChrome = @"Google\Chrome\Application\chrome.exe";
            var pathProgramFiles = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\{pathChrome}";
            var pathProgramFilesX86 = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\{pathChrome}";
            string fileVersionInfo = string.Empty;

            if (File.Exists(pathProgramFilesX86))
            {
                fileVersionInfo = FileVersionInfo.GetVersionInfo(pathProgramFilesX86).ProductVersion;
            }
            else
            {
                fileVersionInfo = FileVersionInfo.GetVersionInfo(pathProgramFiles).ProductVersion;
            }
           
            //string programUri = $"{(string.IsNullOrEmpty(pathProgramFilesX86) ? pathProgramFiles : pathProgramFilesX86)}\\{pathChrome}";
            //FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(programUri);        

            return fileVersionInfo.Split('.').FirstOrDefault();
        }

        /// <summary>
        /// Obtiene la ultima version del driver
        /// </summary>
        /// <param name="processName">Nombre del proceso</param>
        private static string GetLastReleaseVersion()
        {
            string URL = string.Format("https://chromedriver.storage.googleapis.com/LATEST_RELEASE_{0}", GetChromeVersion());
            
            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using var reader = new StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII);
            return reader.ReadToEnd();
        }
    }
}
