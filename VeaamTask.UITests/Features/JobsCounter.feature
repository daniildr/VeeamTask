Feature: Jobs Counter

@UI
Scenario: Check for changes in count
	Given I have opened the career page
	When I set country - Romania
	And I set languages
	| language |
	| English  |
	And I click the show more button
	Then Expected jobs count equal - 30
	And Actual jobs count equal - 30

@UI
Scenario: Check of initial values
	Given I have opened the career page
	When I remember the number of vacancies
	And I set country - Romania
	And I click the show more button
	And I set country - Russian Federation
	And I click the show more button
	Then Expected number of vacancies is equal to the initial 

@UI
Scenario: End to end case
	Given I have opened the career page
	When I remember the number of vacancies
	When I set country - Argentina
	And I set languages
	| launguag |
	| English  |
	Then Expected jobs count equal - 1
	And Actual jobs count equal - 1
	When I set country - Russian Federation
	And I unset languages
	| launguag |
	| English  |
	And I click the show more button
	Then Expected number of vacancies is equal to the initial 
	