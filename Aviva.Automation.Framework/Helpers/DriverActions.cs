using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Configuration;

namespace Aviva.Automation.Framework
{
    public class DriverActions
    {
        /// <summary>
        /// Gets the current instance of <see cref="IWebDriver"/>.
        /// </summary>
        private IWebDriver Driver { get; set; }

        /// <summary>
        /// An expectation to initializes a new instance of Driver class.
        /// </summary>
        public IWebDriver InitializeDriver()
        {
            string browserName = string.Empty;
            try
            {
                string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                browserName = ConfigurationManager.AppSettings["BrowserName"].ToLower(CultureInfo.CurrentCulture);
                switch (browserName)
                {
                    case "chrome":
                        Driver = new ChromeDriver(directoryPath);
                        break;
                    case "ie":
                        Driver = new InternetExplorerDriver(directoryPath);
                        break;
                    case "firefox":
                        Driver = new FirefoxDriver();
                        break;
                    case "safari":
                        Driver = new SafariDriver(directoryPath);
                        break;
                    default:
                        throw new ArgumentException("Browser: \"" + browserName + "\", does not exist in framework.");
                }
                SetupTimeouts(Driver);
            }
            catch (Exception ex)
            {
                CustomExceptionHandler.CustomException(new InvalidDataException(string.Format(CultureInfo.CurrentCulture, "Unable to initialize driver for '" + browserName + "' webbrowser! {0}", ex.Message), ex));
            }
            return Driver;
        }

        private void SetupTimeouts(IWebDriver webDriver)
        {
            TimeSpan ImplicitWaitTime = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWait"]));
            TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["PageLoadTimeout"]));
            TimeSpan ScriptTimeout = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ScriptTimeout"]));
            webDriver.Manage().Timeouts().ImplicitWait = ImplicitWaitTime;
            webDriver.Manage().Timeouts().PageLoad = PageLoadTimeout;
            webDriver.Manage().Timeouts().AsynchronousJavaScript = ScriptTimeout;
            webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Close the current window, quitting the browser if it is the last window currently open.
        /// </summary>
        public void CloseBrowserInstance()
        {
            try
            {
                if (Driver != null)
                    Driver.Close();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to deletes all cookies from the page.
        /// </summary>
        public void DeleteAllCookies()
        {
            try
            {
                Driver.Manage().Cookies.DeleteAllCookies();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to deletes the cookie with the specified name from the page.
        /// </summary>
        /// <param name="name">The name of the cookie to be deleted.</param>
        public void DeleteCookieNamed(string name)
        {
            try
            {
                Driver.Manage().Cookies.DeleteCookieNamed(name);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to deletes the cookie with the specified name from the page.
        /// </summary>
        /// <param name="name">The name of the cookie to be deleted.</param>
        public ReadOnlyCollection<OpenQA.Selenium.Cookie> GetAllCookies()
        {
            try
            {
                return Driver.Manage().Cookies.AllCookies;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return null;
        }

        /// <summary>
        /// Load a new web page in the current browser window.
        /// </summary>
        /// <param name="pageUrl">The URL to load. It is best to use a fully qualified URL.</param>
        public void NavigateToUrl(string pageUrl)
        {
            try
            {
                Driver.Navigate().GoToUrl(pageUrl);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void RefreshWebPage()
        {
            try
            {
                Driver.Navigate().Refresh();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Retirns url of current window.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUrl()
        {
            try
            {
                return Driver.Url;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return null;
        }

        /// <summary>
        /// Closes current window.
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            try
            {
                Driver.Close();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }
    }
}
