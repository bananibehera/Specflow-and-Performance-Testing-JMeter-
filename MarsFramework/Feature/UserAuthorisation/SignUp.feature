Feature: SignUp
	In order to access the application
	As a user or trader
	I want to register first

	
@SpecflowRegression
Scenario: SignUp
    Given I click on Logout button
	Given I click on Join link in SignIn page
	And I entered the valid data for firstname, lastname, email and password
	When I Click on Join button 
	Then I should be successfully signup 

