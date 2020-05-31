Feature: GoogleSearchFeature
	In order to fetch data from
	Google search results page
	I am perfrming a google search

@Test
@GoogleSearch
Scenario Outline: Google Search
	Given I navigate to Google home page
	And I enter '<keyword>' in search box
	When I click on search button
	Then I should be navigated to 'Aviva - Google Search' page
	And Search results page should display atleast '30' links
	And I should be able to print '5'th link in search results
	Examples: 
	| keyword |
	| Aviva   |
	| ABCDE   |