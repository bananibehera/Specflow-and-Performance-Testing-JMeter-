Feature: Profile Skills add/update/delete 
	As a skill trader
	I want to add/update/delete skill
	In order to update my profile details


	Background: 
	Given I navigate to profile details page
	Given I navigate to skill tab

@SpecFlowRegression
    Scenario: Add skill
	When I add a new skill 
	Then that skill should be displayed on my listings

	Scenario: Delete skill
	When I delete an existing skill
	Then that skill should not be displayed on my listings

	Scenario: Update skill
	When I update an existing skill
	Then that updated skill should be displayed on my listings