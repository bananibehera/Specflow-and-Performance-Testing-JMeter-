Feature: ProfileDescription
	In order to update my profile 
	As a skill trader
	I want to add a short profile description 
	so that
	The people seeking for some skills can look into my profile description 
	
	Background: 
	Given I navigate to profile details page

@SpecFlowRegression
Scenario: Add Description
	When I add a short summary/description  
	Then I should see the short summary/description should be displayed on my profile