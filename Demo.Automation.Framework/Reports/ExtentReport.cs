using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace Demo.Automation.Framework
{
    /// <summary>
    /// This class define methods that logs test results and generates test report.
    /// </summary>
    public class ExtentReport
    {
        private ExtentReports _extent;
        private ThreadLocal<ExtentTest> _test = new ThreadLocal<ExtentTest>();

        private string WorkSpace;
        private string ProjectDirectory;
        private string ProjectPath;

        private IWebDriver Driver { get { return Application.driver; } }

        /// <summary>
        /// Initializes the reporter and logs the test result for a given test suite.
        /// </summary>
        /// <param name="suiteName"> Name of the test suite for which test results has to be logged.</param>
        public void InitializeReport()
        {
            try
            {
                string ReporterTheme = ConfigurationManager.AppSettings["ReporterTheme"];
                string ReportTitle = ConfigurationManager.AppSettings["ReportTitle"];
                string ReportName = ConfigurationManager.AppSettings["ReportName"];

                WorkSpace = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                ProjectDirectory = Environment.GetEnvironmentVariable("RUN_FROM_CMD") == "TRUE" ? Path.GetFullPath(Path.Combine(WorkSpace, @"../../../../")) : Path.GetFullPath(Path.Combine(WorkSpace, @"../../../"));
                ProjectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { "\\bin\\" }, StringSplitOptions.None)[0] + "\\";

                string reportName = "\\TestResultReport_" + DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss_fff") + ".html";
                string reportDirectory = Path.Combine(ProjectPath + "\\ExtentReports");
                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }
                string reportPath = Path.Combine(reportDirectory + reportName);
                string configFilePath = Path.Combine(reportDirectory + "\\ExtentReportConfig.xml");
                Assembly assembly = typeof(Demo.Automation.Framework.ExtentReport).Assembly;

                using (Stream configFileStream = assembly.GetManifestResourceStream("Demo.Automation.Framework.Reports.Config.ExtentReportConfig.xml"))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(configFileStream);

                    if (!ReporterTheme.Equals(string.Empty))
                    {
                        xmlDocument.GetElementsByTagName("theme").Item(0).InnerText = ReporterTheme;
                    }
                    if (!ReportTitle.Equals(string.Empty))
                    {
                        xmlDocument.GetElementsByTagName("documentTitle").Item(0).InnerText = ReportTitle;
                    }
                    if (!ReportName.Equals(string.Empty))
                    {
                        xmlDocument.GetElementsByTagName("reportName").Item(0).InnerText = ReportName;
                    }
                    xmlDocument.Save(configFilePath);
                }

                var htmlReporter = new ExtentHtmlReporter(reportPath);
                htmlReporter.LoadConfig(configFilePath);
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Stops logging the test result and performs a flush for test reporter.
        /// </summary>
        public void CloseReporter()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e);
            }
        }

        /// <summary>
        /// Logs a test as started.
        /// </summary>
        /// <param name="testName"> Name of the test case.</param>
        public void StartTest(string testName)
        {
            _test.Value = _extent.CreateTest(testName);
        }

        /// <summary>
        /// Logs a test as ended.
        /// </summary>
        /// <param name="testName"> Name of the test case.</param>
        public void EndTest(string testName)
        {
            //_extent.RemoveTest(_test.Value);
        }

        /// <summary>
        /// Logs a test as passed.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogPassedTest(string logMessage)
        {
            _test.Value.Pass(logMessage);
        }

        /// <summary>
        /// Logs a test as failed.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogFailedTest(string logMessage)
        {
            _test.Value.Fail(logMessage, AttachScreenshot());
        }

        /// <summary>
        /// Logs a test as skipped.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogSkippedTest(string logMessage)
        {
            _test.Value.Skip(logMessage);
        }

        /// <summary>
        /// Logs a info message.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogInfo(string logMessage)
        {
            _test.Value.Info(logMessage);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogWarning(string logMessage)
        {
            _test.Value.Warning(logMessage);
        }

        /// <summary>
        /// Logs a error message.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogError(string logMessage)
        {
            _test.Value.Error(logMessage);
        }

        /// <summary>
        /// Logs a fatal error,
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void LogFatal(string logMessage)
        {
            _test.Value.Fatal(logMessage);
        }

        /// <summary>
        /// Logs screenshot as log.
        /// </summary>
        /// <param name="logMessage"> Message to be logged.</param>
        public void TakeScreenshot()
        {
            _test.Value.Info("Screenshot :", AttachScreenshot());
        }

        /// <summary>
        /// Method to take screenshot and return it base-64 string.
        /// </summary>
        /// <returns></returns>
        private MediaEntityModelProvider AttachScreenshot()
        {
            MediaEntityModelProvider media = null;
            try
            {
                var base64Screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
                media = MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Screenshot).Build();
                return media;
            }
            catch (Exception e)
            {
                CustomExceptionHandler.CustomException(e, "Error while taking screenshot in extentreport.");
            }
            return media;
        }
    }
}