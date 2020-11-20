Feature: SearchSkill
	In order to search different skills
	As a skill trader
	I want to search skill based on the user and skills available

	Background: Navigate to profile page
	Given I navigate to profile details page

@SpecflowRegression
Scenario: Search Skill by Category and subcategory
	When I search a skill 
	Then that skill related result should displayed in category and subcategory

	Scenario:  Search skill by Filter options
	When I search a skill using refine search textbox
	And I Click on any Filter option
	Then I should be able the see the result displayed based on the filter option

	Scenario: Search Skill by user
	When I search a skill based on the registered user
	Then I should be able to see the skills result listed by that user