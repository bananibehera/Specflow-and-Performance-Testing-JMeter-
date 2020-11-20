Feature: ChangePassword
	In order to update my account password
	As a skill trader
	I want to perform change password operation in my profile

	Background: 
	Given I navigate to change password page

@Regression
    Scenario: Change Password
	When I enter my updated password information detail and save it
	And I click on Signout button
	Then I should be able to login again with my updated password successfully