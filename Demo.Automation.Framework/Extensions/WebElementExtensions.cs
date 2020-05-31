using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Demo.Automation.Framework
{
    public static class WebElementExtensions
    {
        #region Methods to deal with element state -> Displayed, Enabled, Selected

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="bool"/> True if element is displayed or false if element is not displayed.</returns>
        public static bool IsDisplayedExtension(this IWebElement element)
        {
            bool flag = false;
            try
            {
                flag = element.Displayed;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while checking whether element: " + element + " is displayed or not.");
            }
            return flag;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="bool"/> True if element is enabled or false if element is not enabled.</returns>
        public static bool IsEnabledExtension(this IWebElement element)
        {
            bool flag = false;
            try
            {
                flag = element.Enabled;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while checking whether element: " + element + " is enabled or not.");
            }
            return flag;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is selected.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="bool"/> True if element is selected or false if element is not selected.</returns>
        public static bool IsSelectedExtension(this IWebElement element)
        {
            bool flag = false;
            try
            {
                flag = element.Selected;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while checking whether element: " + element + " is selected or not.");
            }
            return flag;
        }

        #endregion

        #region Methods to deal with mouse operations

        /// <summary>
        /// Performs a click operation on the given WebElement.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void ClickExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                ScrollToElementExtension(element);
                element.Click();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method in clicking element: " + element);
            }
        }

        /// <summary>
        /// Performs Selenium Actions click on the given WebElement.
        /// </summary>
        /// <param name="element"><see cref="IWebElement"/> WebElement</param>
        public static void ActionClickExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                GetActionsObj().MoveToElement(element).Click().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while clicking element: " + element + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs a double click on the given WebElement.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void DoubleClickExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                GetActionsObj().DoubleClick(element).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while double clicking element: " + element + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs mouse right click on the given WebElement.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void ContextClickExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                GetActionsObj().ContextClick(element).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while right clicking element: " + element + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs a mouse click on the given WebElement using javascript.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void JavascriptClickExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                driver.ExecuteJavaScript("arguments[0].click();", new object[] { element });
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while clicking element: " + element + " (using javaScript)");
            }
        }

        /// <summary>
        /// Performs mouse hover on the given WebElement.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void MouseHoverExtension(this IWebElement element)
        {
            try
            {
                GetActionsObj().MoveToElement(element).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while performing mouse hover on element: " + element);
            }
        }

        #endregion

        #region Methods to work with SendKeys

        /// <summary>
        /// Simulates typing text into the given element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <param name="value"> <see cref="string"/> Value to be entered.</param>
        public static void SendKeysExtension(this IWebElement element, string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ScrollToElementExtension(element);
                    element.SendKeys(value);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while entering text:" + value + ", in element: " + element);
            }
        }

        /// <summary>
        /// Simulates a key stroke into the given element. (Usage: Keys.Control, Keys.Enter, Keys.Backspace, Keys.ArrowUp, etc..)
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <param name="key"> <see cref="Keys"/> Keys.Enter, Keys.Esc, etc.. </param>
        public static void PressKeyExtension(this IWebElement element, string key)
        {
            try
            {
                GetActionsObj().SendKeys(element, key).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to press Key:" + key + ", in element:" + element);
            }
        }

        #endregion

        #region Methods to deal with drop-downs

        /// <summary>
        /// Select all options by the text displayed.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <param name="text"> <see cref="string"/> Option to be selected.</param>
        public static void SelectByVisibleTextExtension(this IWebElement element, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    ScrollToElementExtension(element);
                    SelectElement sel = new SelectElement(element);
                    sel.SelectByText(text);
                }
                catch (Exception e)
                {
                    CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to select from drop-down:" + element + ", using visible text:" + text);
                }
            }
        }

        /// <summary>
        /// Select an option by the value.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <param name="value"> <see cref="string"/> Value option to be selected.</param>
        public static void SelectByValueExtension(this IWebElement element, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    ScrollToElementExtension(element);
                    SelectElement sel = new SelectElement(element);
                    sel.SelectByValue(value);
                }
                catch (Exception e)
                {
                    CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to select from drop-down:" + element + ", using value:" + value);
                }
            }
        }

        /// <summary>
        /// Select the option by the index, as determined by the "index" attribute of the element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <param name="index"> <see cref="int"/> Index option to be selected.</param>
        public static void SelectByIndexExtension(this IWebElement element, int index)
        {
            try
            {
                ScrollToElementExtension(element);
                SelectElement sel = new SelectElement(element);
                sel.SelectByIndex(index);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to select from drop-down:" + element + ", using index:" + index);
            }
        }

        /// <summary>
        /// Select the option randomly using index, as determined by the "index" attribute of the element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void SelectByRandoxIndexExtension(this IWebElement element)
        {
            int index = 0;
            try
            {
                ScrollToElementExtension(element);
                SelectElement sel = new SelectElement(element);
                int max = 0, min = 2;
                max = sel.Options.Count;
                if (max < min)
                {
                    CustomExceptionHandler.CustomException(new Exception(element.ToString() + " Drop down is empty"));
                }
                Random r = new Random();
                index = r.Next(max - min) + min;
                sel.SelectByIndex(index);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to select from drop-down:" + element + ", using random index:" + index);
            }
        }

        /// <summary>
        /// Gets the first selected option within the select element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="string'"/> Selected option within the drop-down element.</returns>
        public static string GetSelectedOptionExtension(this IWebElement element)
        {
            string selectedOption = string.Empty;
            try
            {
                ScrollToElementExtension(element);
                SelectElement sel = new SelectElement(element);
                selectedOption = sel.AllSelectedOptions[0].Text.Trim();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to get selected option from drop-down:" + element);
            }
            return selectedOption;
        }

        /// <summary>
        /// Gets all of the selected options within the select element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="IList{IWebElement}'"/> List of selected option within the drop-down element.</returns>
        public static IList<IWebElement> GetAllOptionsExtension(this IWebElement element)
        {
            IList<IWebElement> options = new List<IWebElement>();
            try
            {
                ScrollToElementExtension(element);
                SelectElement sel = new SelectElement(element);
                options = sel.Options;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to get all options from drop-down:" + element);
            }
            return options;
        }

        #endregion

        #region Methods to deal with Alert and window

        /// <summary>
        /// Select a frame using its previously located <see cref="OpenQA.Selenium.IWebElement"/>
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void SwitchToIFrameExtension(this IWebElement element)
        {
            try
            {
                IWebDriver driver = Application.driver;
                driver.SwitchTo().Frame(element);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to switch into frame:" + element);
            }
        }

        #endregion

        #region Methods to deal with HTML text, attribute

        /// <summary>
        /// Gets the innerText of this element, without any leading or trailing whitespace, and with other whitespace collapsed.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        /// <returns><see cref="string'"/> Inner text of the element.</returns>
        public static string GetTextExtension(this IWebElement element)
        {
            string text = null;
            try
            {
                text = element.Text;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to get text from element:" + element);
            }
            return text;
        }

        /// <summary>
        /// Gets the value of the specified attribute for this element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static string GetAttributeValueExtension(this IWebElement element, string attributeName)
        {
            string value = null;
            try
            {
                value = element.GetAttribute(attributeName);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to get attribute: " + attributeName + " value from element:" + element);
            }
            return value;
        }

        #endregion

        #region Methods to deal with check-box

        /// <summary>
        /// Checks a check-box, if already unchecked.
        /// </summary>
        /// <param name="element"> WebElement</param>
        public static void SelectCheckboxExtension(this IWebElement element)
        {
            try
            {
                if (!IsSelectedExtension(element))
                {
                    ClickExtension(element);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to select check-box:" + element);
            }
        }

        /// <summary>
        /// Unchecks a check-box, if already checked.
        /// </summary>
        /// <param name="element"> WebElement</param>
        public static void UnselectCheckboxExtension(this IWebElement element)
        {
            try
            {
                if (IsSelectedExtension(element))
                {
                    ClickExtension(element);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to unSelect check-box:" + element);
            }
        }

        #endregion

        #region Uncategorized Methods

        /// <summary>
        /// Clears the content of this element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void ClearExtension(this IWebElement element)
        {
            try
            {
                element.Clear();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to clear element:" + element);
            }
        }

        /// <summary>
        /// (DO NOT USE, This feature has a open issue) Drags and drops sourceElement to targetElement.
        /// </summary>
        /// <param name="sourceElement"> WebElement that needs to be dragged.</param>
        /// <param name="targetElement"> WebElement till where the sourceElement needs to be dragged.</param>
        public static void DragAndDropExtension(this IWebElement sourceElement, IWebElement targetElement)
        {
            try
            {
                GetActionsObj().DragAndDrop(sourceElement, targetElement).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to drag element:" + sourceElement + ", and drag to element:" + targetElement);
            }
        }

        /// <summary>
        /// Moves the mouse to the specified element.
        /// </summary>
        /// <param name="element"> <see cref="IWebElement"/> WebElement</param>
        public static void ScrollToElementExtension(this IWebElement element)
        {
            try
            {
                GetActionsObj().MoveToElement(element).Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to scroll to element:" + element);
            }
        }

        /// <summary>
        /// Highlights an element on DOM.
        /// </summary>
        /// <param name="element"><see cref="IWebElement"/> WebElement</param>
        public static void HighlightExtension(this IWebElement element)
        {
            bool alertPresent;
            try
            {
                IWebDriver driver = Application.driver;
                alertPresent = (new SeleniumActions()).IsAlertPresent();
                if (!alertPresent)
                {
                    driver.ExecuteJavaScript(@"arguments[0].style.cssText = ""background-color:yellow;""", new object[] { element });
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to highlight element:" + element);
            }
        }

        /// <summary>
        /// UnHighlights an element on DOM.
        /// </summary>
        /// <param name="element"><see cref="IWebElement"/> WebElement</param>
        public static void UnHighlightExtension(this IWebElement element)
        {
            bool alertPresent;
            try
            {
                IWebDriver driver = Application.driver;
                alertPresent = (new SeleniumActions()).IsAlertPresent();
                if (!alertPresent)
                {
                    driver.ExecuteJavaScript(@"arguments[0].style.cssText = ""background-color:none;""", new object[] { element });
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to unHighlight element:" + element);
            }
        }

        /// <summary>
        /// Initializes a new instance of the OpenQA.Selenium.Interactions.Actions class.
        /// </summary>
        /// <returns> Object of <see cref="Actions"/> class.</returns>
        private static Actions GetActionsObj()
        {
            Actions actions = null;
            try
            {
                IWebDriver driver = Application.driver;
                actions = new Actions(driver);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in extension method while trying to initializes a new instance of the OpenQA.Selenium.Interactions.Actions class.");
            }
            return actions;
        }

        #endregion
    }
}
