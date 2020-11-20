Feature: ProfileAvailabilityDetail
	In order to provide my availability details
	As a skill trader
	I want to add my availability details to my profile

	Background: 
	Given I navigate to profile details page

@SpecFlowRegression
    Scenario: Add the Availability
	When I select my availability option
	Then I should see the selected availability details displayed on my profile

	Scenario: Add the hours
	When I select my hours option 
	Then I should see the selected hours details displayed on my profile

	Scenario: Add the Earn Target details
	When I select my Earn Target option
	Then I should see the selected Earn Target details displayed on my profile