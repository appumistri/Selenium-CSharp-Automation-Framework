using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

namespace Aviva.Automation.Framework
{
    public class FileUtils
    {
        /// <summary>
        /// Local path where file will be downloaded, by default points to Temp folder of current user.
        /// </summary>
        public string _localDownloadPath = Path.GetTempPath();

        /// <summary>
        /// Determines whether http request should follow redirects while downloading.
        /// </summary>
        public bool _followRedirects = true;

        /// <summary>
        /// Determines status of http request of last download attempt.
        /// </summary>
        public string _httpStatusOfLastDownloadAttempt;

        /// <summary>
        /// Download the file specified in the href/src attribute of a WebElement
        /// </summary>
        /// <param name="locator">By Locator of the web element.</param>
        /// <param name="attribute">Attribute that holds the URL of the file (Optional). By default points to "href" attribute.</param>
        /// <param name="destinationPath">Destination path of file (Optional). By default points to Temp folder of current user.</param>
        /// <returns>Absolute path of downloaded file.</returns>
        public string DownloadFile(By locator, string attribute = "href", string destinationPath = "")
        {
            string fileUrl = "";
            try
            {
                if (!string.IsNullOrEmpty(destinationPath))
                {
                    _localDownloadPath = destinationPath;
                }

                IWebElement element = (new SeleniumActions()).FindElement(locator);
                fileUrl = element.GetAttribute(attribute);
                return Downloader(fileUrl, destinationPath);
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }

        /// <summary>
        ///  Download file using url of the file.
        /// </summary>
        /// <param name="fileUrl">URL of the file (Optional). By default takes current url.</param>
        /// <param name="destinationPath">Destination path of file (Optional). By default points to Temp folder of current user.</param>
        /// <returns>Absolute path of downloaded file.</returns>
        public string DownloadFile(string fileUrl = "", string destinationPath = "")
        {
            string url;
            try
            {
                if (!string.IsNullOrEmpty(fileUrl))
                {
                    url = fileUrl;
                }
                else
                {
                    url = (new DriverActions()).GetCurrentUrl();
                }
                return Downloader(fileUrl, destinationPath);
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }

        /// <summary>
        /// Download the file specified in the href attribute of a WebElement
        /// </summary>
        /// <param name="fileUrl">URL of the file.</param>
        /// <param name="destinationPath">Destination path of file (Optional). By default points to Temp folder of current user.</param>
        /// <returns>Absolute path of downloaded file.</returns>
        private string Downloader(string fileUrl, string destinationPath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(fileUrl))
                {
                    throw new NullReferenceException("The element you have specified does not link to anything!");
                }

                Uri url = new Uri(fileUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Host = url.Host;
                request.AllowAutoRedirect = _followRedirects;
                ReadOnlyCollection<OpenQA.Selenium.Cookie> driverCookies = (new DriverActions()).GetAllCookies();
                request.CookieContainer = new CookieContainer();
                System.Net.Cookie cookie;

                foreach (OpenQA.Selenium.Cookie driverCookie in driverCookies)
                {
                    cookie = new System.Net.Cookie(driverCookie.Name, driverCookie.Value, driverCookie.Path, driverCookie.Domain);
                    request.CookieContainer.Add(cookie);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _httpStatusOfLastDownloadAttempt = response.StatusCode.ToString();
                string fileAbsolutePath = _localDownloadPath + DateTime.Now.ToString("ddMMyyyyffff") + url.LocalPath.Replace("/", "").Replace("\\", "");

                using (FileStream outputStream = new FileStream(fileAbsolutePath, FileMode.Create))
                {
                    Stream inputStream = response.GetResponseStream();
                    inputStream.CopyTo(outputStream);
                    inputStream.Flush();
                    inputStream.Close();
                    outputStream.Flush();
                    outputStream.Close();
                }
                return fileAbsolutePath;
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
            return null;
        }

        /// <summary>
        /// Checks whether the file got downloaded or not.
        /// </summary>
        /// <param name="fileAbsolutePath">Absolute path of the file.</param>
        /// <returns>True if file exists, false otherwise.</returns>
        public bool IsFileExists(string fileAbsolutePath)
        {
            return File.Exists(fileAbsolutePath);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileAbsolutePath">Absolute path of the file.</param>
        public void DeleteFile(string fileAbsolutePath)
        {
            try
            {
                File.Delete(fileAbsolutePath);
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(ex);
            }
        }
    }
}
