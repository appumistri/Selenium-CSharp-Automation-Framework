using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Aviva.Automation.Framework
{
    [Binding]
    public class Hooks : Application
    {
        public static IWebDriver Driver;
        public static DriverActions DriverActions;
        public static SeleniumActions SeleniumActions;
        public static SeleniumWaits SeleniumWaits;
        public static SeleniumVerify SeleniumVerify;
        public static ExtentReport Report;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Driver = driver;
            DriverActions = driverActions;
            SeleniumActions = seleniumActions;
            SeleniumWaits = seleniumWaits;
            SeleniumVerify = seleniumVerify;
            Report = report;
            Report.InitializeReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Report.CloseReporter();
            Dispose();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //To-Do
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            //To-Do
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string featureName = FeatureContext.Current.FeatureInfo.Title;
            string scenarioName = ScenarioContext.Current.ScenarioInfo.Title;
            string logMessage = "<b><i>Executing Scenario</i></b> : " + scenarioName + "<br><b><i>In Feature</i></b> : " + featureName;
            Report.StartTest(scenarioName);
            Report.LogInfo(logMessage);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            string scenarioName = ScenarioContext.Current.ScenarioInfo.Title;
            Exception exception = ScenarioContext.Current.TestError;
            if (exception != null)
            {
                string logMeaage = "<b><i><font color=red>Scenario Failed</font></i></b> : " + scenarioName;
                Report.LogFailedTest(logMeaage);
            }
            else
            {
                string logMessage = "<b><i>Successfully completed Scenario</i></b> : " + scenarioName;
                Report.LogPassedTest(logMessage);
            }
            Report.EndTest(scenarioName);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            string definitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            string definitionContext = ScenarioStepContext.Current.StepInfo.Text;
            string logMessage = "<b><i>Executing Step</i></b> : (" + definitionType + ") " + definitionContext;
            Report.LogInfo(logMessage);
        }

        [AfterStep]
        public void AfterStep()
        {
            string definitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            string definitionContext = ScenarioStepContext.Current.StepInfo.Text;
            string logMessage = "<b><i>Executed Step</i></b> : (" + definitionType + ") " + definitionContext;
            Report.LogInfo(logMessage);
        }
    }
}
