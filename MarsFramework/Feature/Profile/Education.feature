Feature: Profile education details add/update/delete 
	As a skill trader
	I want to add/update/delete education details 
	In order to update my profile details

	Background: 
	Given I navigate to profile details page
	Given I navigate to Education tab

@SpecFlowRegression
    Scenario: Add education details
	When I add a new education detail
	Then that education details should be displayed on my listings

	Scenario: Delete education details
	When I delete an existing education
	Then that education details should not be displayed on my listings

	Scenario: Update education details
	When I update an existing education details
	Then that updated education details should be displayed on my listings