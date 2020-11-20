Feature: Profile certification add/update/delete 
	As a skill trader
	I want to add/update/delete certification details 
	In order to update my profile details

	Background: 
	Given I navigate to profile details page
	Given I navigate to certification tab

@mytag
    Scenario: Add certification details
	When I add a new certification detail
	Then that certification details should be displayed on my listings

	Scenario: Delete certification details
	When I delete an existing certification
	Then that certification details should not be displayed on my listings

	Scenario: Update certification details
	When I update an existing certification details
	Then that updated certification details should be displayed on my listings