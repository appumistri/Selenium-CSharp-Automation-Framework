using System.Threading;
using OpenQA.Selenium;

namespace Aviva.Automation.Framework
{
    public class Application
    {
        private static ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();
        private static ThreadLocal<DriverActions> _driverActions = new ThreadLocal<DriverActions>();
        private static ThreadLocal<SeleniumActions> _seleniumActions = new ThreadLocal<SeleniumActions>();
        private static ThreadLocal<SeleniumWaits> _seleniumWaits = new ThreadLocal<SeleniumWaits>();
        private static ThreadLocal<SeleniumVerify> _seleniumVerify = new ThreadLocal<SeleniumVerify>();
        private static ThreadLocal<ExtentReport> _extentReport = new ThreadLocal<ExtentReport>();

        public static IWebDriver driver
        {
            get
            {
                if (!_driver.IsValueCreated)
                {
                    _driver.Value = driverActions.InitializeDriver();
                }
                return _driver.Value;
            }
        }

        public static DriverActions driverActions
        {
            get
            {
                if (!_driverActions.IsValueCreated)
                {
                    _driverActions.Value = new DriverActions();
                }
                return _driverActions.Value;
            }
        }

        public static SeleniumActions seleniumActions
        {
            get
            {
                if (!_seleniumActions.IsValueCreated)
                {
                    _seleniumActions.Value = new SeleniumActions();
                }
                return _seleniumActions.Value;
            }
        }

        public static SeleniumWaits seleniumWaits
        {
            get
            {
                if (!_seleniumWaits.IsValueCreated)
                {
                    _seleniumWaits.Value = new SeleniumWaits();
                }
                return _seleniumWaits.Value;
            }
        }

        public static SeleniumVerify seleniumVerify
        {
            get
            {
                if (!_seleniumVerify.IsValueCreated)
                {
                    _seleniumVerify.Value = new SeleniumVerify();
                }
                return _seleniumVerify.Value;
            }
        }

        public static ExtentReport report
        {
            get
            {
                if (!_extentReport.IsValueCreated)
                {
                    _extentReport.Value = new ExtentReport();
                }
                return _extentReport.Value;
            }
        }

        public static void Dispose()
        {
            if (!_driver.Value.ToString().Contains("null"))
            {
                _driver.Value.Quit();
            }
            _driver.Dispose();
            _driverActions.Dispose();
            _seleniumActions.Dispose();
            _seleniumWaits.Dispose();
            _seleniumVerify.Dispose();
            _extentReport.Dispose();
        }

        public static T NewPage<T>() where T : PageBase, new()
        {
            var page = new T
            {
                Driver = driver,
                DriverActions = driverActions,
                SeleniumActions = seleniumActions,
                SeleniumWaits = seleniumWaits,
                SeleniumVerify = seleniumVerify,
                Report = report
            };
            return page;
        }
    }
}
