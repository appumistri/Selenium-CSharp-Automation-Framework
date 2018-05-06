using System;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aviva.Automation.Framework
{
    public class GoogleSearchResultsPage : PageBase
    {
        private readonly By SearchResultLink = By.XPath("//a[contains(@href,'aviva')]");

        private static ReadOnlyCollection<IWebElement> SearchResultLinks = null;

        public void NavigatedToGoogleSearchResultsPage(string pageTitle)
        {
            SeleniumVerify.ExactPageTitle(pageTitle);
        }

        public void VerifyNumberOfSearchResultsLink(int expectedNumberOfLinks)
        {
            SearchResultLinks = SeleniumActions.FindElements(SearchResultLink);
            Assert.IsTrue(expectedNumberOfLinks < SearchResultLinks.Count, "Actual number of links: " + SearchResultLinks.Count + " is less than the expected number of links: " + expectedNumberOfLinks);
        }

        public void PrintLink(int IndexOfLinkToBePrinted)
        {
            string linkText = SearchResultLinks.ElementAt(IndexOfLinkToBePrinted - 1).Text;
            Console.WriteLine(IndexOfLinkToBePrinted + "'th link in the result is: " + linkText);
        }
    }
}
