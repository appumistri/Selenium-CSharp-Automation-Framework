using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace Demo.Automation.Framework
{
    /// <summary>
    /// This Enum defines different type of waits available, that can be passed as a paramer to a wait method to specify the level of wait.
    /// </summary>
    public enum WaitType
    {
        Small,
        Medium,
        Large,
        ImplicitWaitTime
    }

    /// <summary>
    /// This class defines variety of waits required when performing different actions on a webpage.
    /// </summary>
    public class SeleniumWaits
    {
        /// <summary>
        /// this property returns the current instance of <see cref="IWebDriver"/> from <see cref="TestEnvironment"/> class.
        /// </summary>
        private IWebDriver Driver { get { return Application.driver; } set { } }

        /// <summary>
        /// This method returns wait time based on the specified wait type.
        /// </summary>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        /// <returns><see cref="TimeSpan"/> wait time based on <see cref="WaitType"/> specified.</returns>
        public TimeSpan GetWait(WaitType waitType)
        {
            TimeSpan waitTime = TimeSpan.FromSeconds(0);

            try
            {
                switch (waitType)
                {
                    case WaitType.Small:
                        waitTime = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["SmallWait"]));
                        break;
                    case WaitType.Medium:
                        waitTime = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["MediumWait"]));
                        break;
                    case WaitType.Large:
                        waitTime = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["LargeWait"]));
                        break;
                    case WaitType.ImplicitWaitTime:
                        waitTime = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                        break;
                    default:
                        throw new Exception("Undefined Wait Type.");
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return waitTime;
        }

        /// <summary>
        /// An expectation for wait and check whether the given frame is available to switch to. If the frame is available it switches the driver to the specified frame.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name = "waitType" > Type of wait from <see cref="WaitType"/>.</param>
        public void WaitAndSwitchToFrame(By locator, WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Timeout exception while waiting for frame: " + locator);
            }
        }

        /// <summary>
        /// An expectation to wait and check for attribute to be present in current web element.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementAttributeToBePresent(By locator, string attributeName, WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(drv => (drv.FindElement(locator).GetAttribute(attributeName)));
            }
            catch (Exception e)
            {
                string message = string.Format(" Locator : '{0}' and trying to get attribute '{1}'", locator, attributeName);
                CustomExceptionHandler.CustomException(e, message);
            }
        }

        /// <summary>
        /// An expectation to wait and check an element is visible and enabled such that it can be clicked.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementToBeClickable(By locator, WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception e)
            {
                string message = string.Format(" Locator : '{0}'", locator);
                CustomExceptionHandler.CustomException(e, message);
            }
        }

        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a page. This does not necessarily mean that the element is visible or enabled.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementToBePresent(By locator, WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                string message = string.Format(" Locator : '{0}'", locator);
                CustomExceptionHandler.CustomException(e, message);
            }
        }

        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a page and visible.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementToBeVisible(By locator, WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception e)
            {
                string message = string.Format(" Locator : '{0}'", locator);
                CustomExceptionHandler.CustomException(e, message);
            }
        }

        /// <summary>
        /// An expectation to wait and check that the web element with text is either invisible or not present on the DOM.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="text">The text of an element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementWithTextToBeInvisible(By locator, string text, WaitType waitType)
        {
            bool result = false;
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                result = wait.Until(ExpectedConditions.InvisibilityOfElementWithText(locator, text));
                if (!result)
                    throw new WebDriverTimeoutException();
            }
            catch (WebDriverTimeoutException e)
            {
                string message = string.Format(" Failed due to the text '{0}' in the element '{1}' is not disappeared in given wait time", text, locator);
                CustomExceptionHandler.CustomException(e, message);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to wait and check that the given text is present in the given locator.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="text">The text of an element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForTextToBePresentInElement(By locator, string text, WaitType waitType)
        {
            bool result = false;
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                result = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
                if (!result)
                    throw new WebDriverTimeoutException();
            }
            catch (WebDriverTimeoutException e)
            {
                string message = string.Format(" The text '{0}' is not present in the element '{1}' within given wait time", text, locator);
                CustomExceptionHandler.CustomException(e, message);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to wait and check an element is either invisible or not present in the DOM.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementToBeInvisible(By locator, WaitType waitType)
        {
            bool result = false;
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                if (!result)
                    throw new WebDriverTimeoutException();
            }
            catch (WebDriverTimeoutException e)
            {
                string message = string.Format(" The element '{0}' is not disappeared within given wait time", locator);
                CustomExceptionHandler.CustomException(e, message);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Waits for a webpage to load, considering Ajax, Angular, JavaScript and any other background network calls.
        /// </summary>
        public void WaitForPageLoad()
        {
            try
            {
                WaitForBrowserLoad();
                WaitForAjaxLoad();
                WaitForNetworkCalls();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to wait for browser load action to be completed.
        /// </summary>
        public void WaitForBrowserLoad()
        {
            string state = string.Empty;
            TimeSpan TimeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["PageLoadTimeout"]));
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeOut);
                wait.Until(drv =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)Driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));
                });
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to wait for ajax load to be completed.
        /// </summary>
        public void WaitForAjaxLoad()
        {
            try
            {
                bool jQueryDefined = (bool)((IJavaScriptExecutor)Driver).ExecuteScript(@"return typeof jQuery != 'undefined'");
                if (jQueryDefined)
                {
                    TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["PageLoadTimeout"]));
                    WebDriverWait wait = new WebDriverWait(Driver, timeOut);
                    wait.Until(driver => (bool)((IJavaScriptExecutor)Driver).ExecuteScript(@"return jQuery.active == 0"));
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// An expectation to wait for angular load to be completed.
        /// </summary>
        /*public void WaitForAngularLoad()
        {
            try
            {
                string script = @"return window.angular.element(document.body).injector().get('$http').pendingRequests.length == 0";
                bool angularReady = (bool)((IJavaScriptExecutor)Driver).ExecuteScript(@"return window.angular != undefined");
                if (angularReady)
                {
                    TimeSpan timeOut = TestEnvironment.WebBrowserProperty.PageLoadTimeOut;
                    WebDriverWait wait = new WebDriverWait(Driver, timeOut);
                    wait.Until(driver => (bool)((IJavaScriptExecutor)Driver).ExecuteScript(script));
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }*/

        /// <summary>
        /// An expectation to wait for network calls to be finished.
        /// </summary>
        public void WaitForNetworkCalls()
        {
            string activeCalls = "0";

            string script = @"source:(" +
                "function(){" +
                "var send = XMLHttpRequest.prototype.send;" +
                "var release = function(){ --XMLHttpRequest.active };" +
                "var onloadend = function(){ setTimeout(release, 1) };" +
                "XMLHttpRequest.active = 0;" +
                "XMLHttpRequest.prototype.send = function() {" +
                "++XMLHttpRequest.active;" +
                "this.addEventListener('loadend', onloadend, true);" +
                "send.apply(this, arguments);" +
                "};})();";

            ((IJavaScriptExecutor)Driver).ExecuteScript(script);
            TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["PageLoadTimeout"]));
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, timeOut);
                wait.Until(driver =>
                {
                    try
                    {
                        activeCalls = ((IJavaScriptExecutor)Driver).ExecuteScript(@"return XMLHttpRequest.active").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        // Ignore
                    }
                    bool flag = activeCalls.Equals("0", StringComparison.InvariantCultureIgnoreCase);
                    return flag;
                });
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Wait until an element is no longer attached to the DOM.
        /// </summary>
        /// <param name="locator">Locator for the web element.</param>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForElementStaleness(By locator, WaitType waitType)
        {
            bool result = false;
            try
            {
                TimeSpan time = GetWait(waitType);
                IWebElement elem = (new SeleniumActions()).FindElement(locator);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                result = wait.Until(ExpectedConditions.StalenessOf(elem));
                if (!result)
                    throw new WebDriverTimeoutException();
            }
            catch (WebDriverTimeoutException e)
            {
                string message = string.Format(" The element '{0}' is still attached with DOM in given wait time: '{1}'", locator, waitType.ToString());
                CustomExceptionHandler.CustomException(e, message);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        ///  Waits for alert to be present.
        /// </summary>
        /// <param name="waitType">Type of wait from <see cref="WaitType"/>.</param>
        public void WaitForAlertToBePresent(WaitType waitType)
        {
            try
            {
                TimeSpan time = GetWait(waitType);
                WebDriverWait wait = new WebDriverWait(Driver, time);
                wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }
    }
}
