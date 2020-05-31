using System.Configuration;
using OpenQA.Selenium;

namespace Demo.Automation.Framework
{
    public class GoogleHomePage : PageBase
    {
        private readonly By GoogleSearchBox = By.Id("lst-ib");
        private readonly By GoogleSearchButton = By.Name("btnK");

        public void NavigateToGoogleHomePage()
        {
            Driver.Url = ConfigurationManager.AppSettings["AppUrl"];
            SeleniumWaits.WaitForPageLoad();
            SeleniumVerify.ExactPageTitle("Google");
        }

        public void EnterKeywordInGoogleSearchBox(string keyword)
        {
            SeleniumActions.Clear(GoogleSearchBox);
            SeleniumActions.SendKeys(GoogleSearchBox, keyword);
            SeleniumActions.PressKey(GoogleSearchBox, Keys.Tab);
        }

        public void ClickOnGoogleSearchButton()
        {
            SeleniumActions.Click(GoogleSearchButton);
            SeleniumWaits.WaitForPageLoad();
        }
    }
}
