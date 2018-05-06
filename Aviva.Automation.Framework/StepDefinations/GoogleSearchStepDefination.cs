using TechTalk.SpecFlow;

namespace Aviva.Automation.Framework
{
    [Binding]
    public sealed class GoogleSearchStepDefination : Application
    {
        [Given(@"I navigate to Google home page")]
        public void GivenINavigateToGoogleHomePage()
        {
            var page = NewPage<GoogleHomePage>();
            page.NavigateToGoogleHomePage();
        }

        [Given(@"I enter '(.*)' in search box")]
        public void GivenIEnterInSearchBox(string keyword)
        {
            var page = NewPage<GoogleHomePage>();
            page.EnterKeywordInGoogleSearchBox(keyword);
        }

        [When(@"I click on search button")]
        public void WhenIClickOnSearchButton()
        {
            var page = NewPage<GoogleHomePage>();
            page.ClickOnGoogleSearchButton();
        }

        [Then(@"I should be navigated to '(.*)' page")]
        public void ThenIShouldBeNavigatedToPage(string pageTitle)
        {
            var page = NewPage<GoogleSearchResultsPage>();
            page.NavigatedToGoogleSearchResultsPage(pageTitle);
        }

        [Then(@"Search results page should display atleast '(.*)' links")]
        public void ThenSearchResultsPageShouldDisplayAtleastLinks(int expectedNumberOflinks)
        {
            var page = NewPage<GoogleSearchResultsPage>();
            page.VerifyNumberOfSearchResultsLink(expectedNumberOflinks);
        }

        [Then(@"I should be able to print '(.*)'th link in search results")]
        public void ThenIShouldBeAbleToPrintThLinkInSearchResults(int IndexOfLinkToBePrinted)
        {
            var page = NewPage<GoogleSearchResultsPage>();
            page.PrintLink(IndexOfLinkToBePrinted);
        }


    }
}
