Feature: Jobs Counter

@UI
Scenario: Check Jobs Counter
	Given I have opened the career page
	When I set country - Romania
	And I set languages
	| launguag |
	| English  |
	And I click the show more button
	Then Expected jobs count equal - 30
	And Actual jobs count equal - 30

@UI
Scenario: Check Jobs Counter2
	Given I have opened the career page
	When I remember the number of vacancies
	And I set country - Romania
	And I click the show more button
	And I set country - Russian Federation
	And I click the show more button
	Then Expected number of vacancies is equal to the initial 