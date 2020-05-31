using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Demo.Automation.Framework
{
    /// <summary>
    /// This class will provide all the generic methods to performed user action on page.
    /// </summary>
    public class SeleniumActions
    {
        #region Methods to deal with element state -> Displayed, Enabled, Selected

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="bool"/> True if element is displayed or false if element is not displayed.</returns>
        public bool IsDisplayed(By locator)
        {
            bool flag = false;
            IWebElement element = null;
            try
            {
                element = FindElement(locator);
                flag = element.IsDisplayedExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while checking whether locator:" + locator + " is displayed or not.");
            }
            return flag;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="bool"/> True if element is enabled or false if element is not enabled.</returns>
        public bool IsEnabled(By locator)
        {
            bool flag = false;
            IWebElement element = null;
            try
            {
                element = FindElement(locator);
                flag = element.IsEnabledExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while checking whether locator:" + locator + " is enabled or not.");
            }
            return flag;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is selected.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="bool"/> True if element is selected or false if element is not selected.</returns>
        public bool IsSelected(By locator)
        {
            bool flag = false;
            IWebElement element = null;
            try
            {
                element = FindElement(locator);
                flag = element.IsSelectedExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while checking whether locator:" + locator + " is selected or not.");
            }
            return flag;
        }

        #endregion

        #region Methods to deal with mouse operations

        /// <summary>
        /// Performs a click operation on the given WebElement.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void Click(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.ClickExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in clicking element: " + locator);
            }
        }

        /// <summary>
        /// Performs Selenium Actions click on the given WebElement.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void ActionClick(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.ActionClickExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in clicking element: " + locator + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs a double click on the given WebElement.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void DoubleClick(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.DoubleClickExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in double clicking element: " + locator + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs mouse right click on the given WebElement.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void ContextClick(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.ContextClickExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in right clicking element: " + locator + " (using Selenium Actions)");
            }
        }

        /// <summary>
        /// Performs a mouse click on the given WebElement using javascript.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void JavascriptClick(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.JavascriptClickExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in double clicking element: " + locator + " (using javaScript)");
            }
        }

        /// <summary>
        /// Performs an mouse hover on element using its By locator.
        /// </summary>
        /// <param name="locator"> By locator of the element, this parameter is Optional (Web-element should be passed to this method if keeping By Locator as optional)</param>
        public void MouseHover(By locator)
        {
            IWebElement element;
            try
            {
                IWebDriver driver = Application.driver;
                TimeSpan timeOut = TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ImplicitWaitTime"]));
                WebDriverWait wait = new WebDriverWait(driver, timeOut);
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                element = FindElement(locator);
                element.MouseHoverExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in performing mouse hover on element: " + locator);
            }
        }

        /// <summary>
        /// (DO NOT USE, This feature has a open issue) Drags and drops sourceElement to targetElement.
        /// </summary>
        /// <param name="sourceLocator"> Locator that needs to be dragged.</param>
        /// <param name="targetLocator"> Locator till where the sourceLocator needs to be dragged.</param>
        public void DragAndDrop(By sourceLocator, By targetLocator)
        {
            IWebElement source;
            IWebElement target;
            try
            {
                source = FindElement(sourceLocator);
                target = FindElement(targetLocator);
                source.DragAndDropExtension(target);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to drag locator" + sourceLocator + ", and drop to locator" + targetLocator);
            }
        }

        #endregion

        #region Methods to work with SendKeys

        /// <summary>
        /// Simulates typing text into the given element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <param name="value"> <see cref="string"/> Value to be entered.</param>
        public void SendKeys(By locator, string value)
        {
            IWebElement element;
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    element = FindElement(locator);
                    element.SendKeysExtension(value);
                }
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred in entering text:" + value + ", in element: " + locator);
            }
        }

        /// <summary>
        /// Simulates a key stroke into the given element. (Usage: Keys.Control, Keys.Enter, Keys.Backspace, Keys.ArrowUp, etc..)
        /// </summary>
        /// /// <param name="locator"> <see cref="By"/> locator</param>
        /// <param name="key"> <see cref="string"/> key over here is referred to the key intended to be pressed. </param>
        public void PressKey(By locator, string key)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.PressKeyExtension(key);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to press Key:" + key + ", in element:" + locator);
            }
        }

        /// <summary>
        /// Sends a sequence of keystrokes to the browser.
        /// </summary>
        /// <param name="key"> <see cref="string"/> key over here is referred to the key intended to be pressed. </param>
        public void PressKey(string key)
        {
            try
            {
                GetActionsObj().SendKeys(key).Build().Perform();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to press Key:" + key);
            }
        }

        #endregion

        #region Methods to deal with HTML tables

        /// <summary>
        /// Gets the number of rows present in the HTML table.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="int"/> Number of rows available within the locator specified.</returns>
        public int GetRowCount(By locator)
        {
            int rowsCount = 0;
            IWebElement element;
            By row = By.TagName("tr");
            try
            {
                element = FindElement(locator);
                ReadOnlyCollection<IWebElement> rows_table = element.FindElements(row);
                rowsCount = rows_table.Count;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, ("Exception occurred in finding table: " + locator));
            }
            return rowsCount;
        }

        /// <summary>
        /// Gets <see cref="ReadOnlyCollection{IWebElement}"/> of all rows in the HTML table.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="ReadOnlyCollection{IWebElement}"/> Collection of roe web element available within the locator specified.</returns>
        public ReadOnlyCollection<IWebElement> GetTableRows(By locator)
        {
            ReadOnlyCollection<IWebElement> tableRows = null;
            IWebElement element;
            By row = By.TagName("tr");
            try
            {
                element = FindElement(locator);
                tableRows = element.FindElements(row);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, ("Exception occurred in finding table: " + locator));
            }
            return tableRows;
        }

        #endregion

        #region Methods to deal with drop-downs

        /// <summary>
        /// Select all options by the text displayed.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <param name="text"> <see cref="string"/> Text option to be selected.</param>
        public void SelectByVisibleText(By locator, string text)
        {
            IWebElement element;
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    element = FindElement(locator);
                    element.SelectByVisibleTextExtension(text);
                }
                catch (Exception e)
                {
                    CustomExceptionHandler.CustomException(e, "Exception occurred while trying to select from drop-down:" + locator + ", using visible text:" + text);
                }
            }
        }

        /// <summary>
        /// Select an option by the value.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <param name="value"> <see cref="string"/> Value option to be selected.</param>
        public void SelectByValue(By locator, string value)
        {
            IWebElement element;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    element = FindElement(locator);
                    element.SelectByValueExtension(value);
                }
                catch (Exception e)
                {
                    CustomExceptionHandler.CustomException(e, "Exception occurred while trying to select from drop-down:" + locator + ", using value:" + value);
                }
            }
        }

        /// <summary>
        /// Select the option by the index, as determined by the "index" attribute of the element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <param name="Index"> <see cref="int"/> Index option to be selected.</param>
        public void SelectByIndex(By locator, int Index)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.SelectByIndexExtension(Index);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to select from drop-down:" + locator + ", using index:" + Index);
            }
        }

        /// <summary>
        /// Select the option randomly using index, as determined by the "index" attribute of the element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void SelectByRandoxIndex(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.SelectByRandoxIndexExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to select from drop-down:" + locator);
            }
        }

        /// <summary>
        /// Gets the first selected option within the select element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="string'"/> Selected option within the drop-down locator.</returns>
        public string GetSelectedOption(By locator)
        {
            string selectedOption = string.Empty;
            IWebElement element;
            try
            {
                element = FindElement(locator);
                selectedOption = element.GetSelectedOptionExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to get selected option from drop-down:" + locator);
            }
            return selectedOption;
        }

        /// <summary>
        /// Gets all of the selected options within the select element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="IList{IWebElement}'"/> List of selected option within the drop-down locator.</returns>
        public IList<IWebElement> GetAllOption(By locator)
        {
            IWebElement element;
            IList<IWebElement> elems = new List<IWebElement>();
            try
            {
                element = FindElement(locator);
                elems = element.GetAllOptionsExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to get all options from drop-down:" + locator);
            }
            return elems;
        }

        #endregion

        #region Methods to deal with Alert and window

        /// <summary>
        /// Dismiss the alert.
        /// </summary>
        public void DismissAlert()
        {
            try
            {
                Application.driver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to dismiss alert.");
            }
        }

        /// <summary>
        /// Accepts the alert.
        /// </summary>
        public void AcceptAlert()
        {
            try
            {
                Application.driver.SwitchTo().Alert().Accept();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to accept alert.");
            }
        }

        /// <summary>
        /// Gets the text of the alert.
        /// </summary>
        /// <returns>The <see cref="string" />.</returns>
        public string GetAlertText()
        {
            try
            {
                if (IsAlertPresent())
                    return Application.driver.SwitchTo().Alert().Text;
                else
                    return null;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return null;
        }

        /// <summary>
        /// Checks if alert is present or not and switch to alert.
        /// </summary>
        /// <returns><see cref="bool"/> returns true if alert is present, false if not.</returns>
        public bool IsAlertPresent()
        {
            bool flag = false;
            IAlert alert = null;
            try
            {
                alert = Application.driver.SwitchTo().Alert();
                if (alert != null)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Switches to Active Window.
        /// </summary>
        public void SwitchToActiveWindow()
        {
            try
            {
                Application.driver.SwitchTo().ActiveElement();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to switch into active window.");
            }
        }

        /// <summary>
        /// Select a frame using its previously located <see cref="OpenQA.Selenium.IWebElement"/>.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void SwitchToIFrame(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.SwitchToIFrameExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to switch into frame:" + locator);
            }
        }

        #endregion

        #region Methods to deal with findElement

        /// <summary>
        /// Finds the first <see cref="IWebElement"/> using the given <see cref="By"/> mechanism with given RetryCount.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public IWebElement FindElement(By locator)
        {
            IWebElement element = null;
            int retryCount = 0;
            int retry = 0;
            try
            {
                retryCount = retry = !ConfigurationManager.AppSettings["RetryCount"].Equals(string.Empty) ? Convert.ToInt32(ConfigurationManager.AppSettings["RetryCount"]) : 0;
                do
                {
                    try
                    {
                        retry--;
                        element = Application.driver.FindElement(locator);
                    }
                    catch (Exception e)
                    {
                        if (retry > 0)
                        {
                            continue;
                        }
                        else
                        {
                            throw e;
                        }
                    }
                } while ((retry > 0) && (element == null));
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to find element using locator:" + locator + ", with RetryCount:" + retryCount);
            }
            return element;
        }

        /// <summary>
        /// Finds all <see cref="OpenQA.Selenium.IWebElement"/> within the current context using the given mechanism.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> The locating mechanism to use.</param>
        /// <returns>A <see cref="ReadOnlyCollection{IWebElement}"/> of all <see cref="IWebElement"/> matching the current criteria, or an empty list if nothing matches.</returns>
        public ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            try
            {
                elements = Application.driver.FindElements(locator);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to find elements using locator:" + locator);
            }
            return elements;
        }

        #endregion

        #region Methods to deal with HTML text, attribute, Source

        /// <summary>
        /// Gets the innerText of this element, without any leading or trailing whitespace, and with other whitespace collapsed.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        /// <returns><see cref="string'"/> Inner text of the element.</returns>
        public string GetText(By locator)
        {
            string text = null;
            IWebElement element;
            try
            {
                element = FindElement(locator);
                text = element.GetTextExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to get text from element:" + locator);
            }
            return text;
        }

        /// <summary>
        /// Gets the value of the specified attribute for this element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> Locator</param>
        public string GetAttributeValue(By locator, string attributeName)
        {
            string value = null;
            IWebElement element;
            try
            {
                element = FindElement(locator);
                value = element.GetAttributeValueExtension(attributeName);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to get attribute: " + attributeName + " value from element:" + locator);
            }
            return value;
        }

        /// <summary>
        /// Gets source code of the current page.
        /// </summary>
        public string GetPageSource()
        {
            string source = string.Empty;
            try
            {
                source = Application.driver.PageSource;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to get page source.");
            }
            return source;
        }

        #endregion

        #region Methods to deal with check-box

        /// <summary>
        /// Checks a check-box, if already unchecked.
        /// </summary>
        /// <param name="locator"> By locator of the element</param>
        public void SelectCheckbox(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.SelectCheckboxExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to select check-box:" + locator);
            }
        }

        /// <summary>
        /// Unchecks a check-box, if already checked.
        /// </summary>
        /// <param name="locator"> By locator of the element</param>
        public void UnselectCheckbox(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.UnselectCheckboxExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to unSelect check-box:" + locator);
            }
        }

        #endregion

        #region Uncategorized Methods

        /// <summary>
        /// Clears the content of this element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void Clear(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.ClearExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to clear element:" + locator);
            }
        }

        /// <summary>
        /// Moves the mouse to the specified element.
        /// </summary>
        /// <param name="locator"> <see cref="By"/> locator</param>
        public void ScrollToElement(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.ScrollToElementExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to scroll to element:" + locator);
            }
        }

        /// <summary>
        /// Highlights an element on DOM.
        /// </summary>
        /// <param name="locator"><see cref="By"/> locator</param>
        public void Highlight(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.HighlightExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to highlight element:" + locator);
            }
        }

        /// <summary>
        /// UnHilights an element on DOM.
        /// </summary>
        /// <param name="locator"><see cref="By"/> locator</param>
        public void UnHighlight(By locator)
        {
            IWebElement element;
            try
            {
                element = FindElement(locator);
                element.UnHighlightExtension();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to unHilight element:" + locator);
            }
        }

        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        public void PageRefresh()
        {
            try
            {
                Application.driver.Navigate().Refresh();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to refresh the page.");
            }
        }

        ///<summary>
        /// Assemble locator with dynamic int
        /// </summary>
        /// <param name="locator"> Locator of the element as a string</param>
        /// <param name="Int">Dynamic int for element row</param>
        public By AssembleLocator(String locator, int Int)
        {
            By byLocator = By.Id(String.Format(locator, Int));
            return byLocator;
        }

        ///<summary>
        /// Gets parent element.
        /// </summary>
        /// <param name="locator"><see cref="By"/> locator</param>
        /// <returns>Parent element</returns>
        public IWebElement GetParent(By locator)
        {
            IWebElement element = null;
            By parent = By.XPath("..");
            try
            {
                element = FindElement(locator);
                element = element.FindElement(parent);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
            return element;
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
                CustomExceptionHandler.CustomException(e, "Exception occurred while trying to initializes a new instance of the OpenQA.Selenium.Interactions.Actions class.");
            }
            return actions;
        }

        #endregion
    }
}