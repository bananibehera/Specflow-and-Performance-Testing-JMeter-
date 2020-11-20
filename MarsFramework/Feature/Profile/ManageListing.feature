Feature: SkillManageListing
	In order to manage my services 
	As a skill trader
	I want to edit/delete my service details  

	Background: 
	Given I navigate to Manage Listing page

@SpecflowRegression
    Scenario: Edit Share skills service
	When I edit the existing shared skill service details
	Then that service should be edited and updated in the list

	Scenario: Share skills Delete service
	When I delete the existing shared skill service details
	Then that service should not be displayed in the list
