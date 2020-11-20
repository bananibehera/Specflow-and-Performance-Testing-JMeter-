Feature: ShareSkill
	In order to trade my skills
	As a skill trader
	I want to create a new service 

@SpecFlowRegression
    Scenario: Create a new service
	Given I navigate to profile details page
	When  I click on the Share skill button
	Then  I should successfully navigate to service listing form page
	When  I enter all the valid data for the service 
	And   I click on the save button
	Then  I should see the service successfully displayed in the manage listings page


	
