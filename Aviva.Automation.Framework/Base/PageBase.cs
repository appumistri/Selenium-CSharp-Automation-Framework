using OpenQA.Selenium;

namespace Aviva.Automation.Framework
{
    public class PageBase
    {
        public IWebDriver Driver = Application.driver;
        public DriverActions DriverActions = Application.driverActions;
        public SeleniumActions SeleniumActions = Application.seleniumActions;
        public SeleniumWaits SeleniumWaits = Application.seleniumWaits;
        public SeleniumVerify SeleniumVerify = Application.seleniumVerify;
        public ExtentReport Report = Application.report;
    }
}
