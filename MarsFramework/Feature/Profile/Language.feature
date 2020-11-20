Feature: Profile language add/update/delete 
	As a skill trader
	I want to add/update/delete language 
	In order to update my profile details

	Background: 
	Given I navigate to profile details page
	Given I navigate to language tab under profile page
	

   @SpecFlowRegression
    Scenario:  Add a new language 
	When I add a new language 
	Then that language should be displayed on my listings

	
	Scenario: Delete language
	When I delete an existing language
	Then that language should not be displayed on my listings

	
	Scenario: Update languauge
	When I update an existing language
	Then that updated language should be displayed on my listings

	