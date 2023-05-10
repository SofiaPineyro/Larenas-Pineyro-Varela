Feature: SnackMaintenance

@snackmaintenance
Scenario:  Successful snack creation
	Given I am an administrator
	When I add a new snack with name "Chips", description "Potato chips", and price "159"
	Then the response status code should be 200
	And the snack with name "Chips" should be added to the snack list

Scenario: 
	Given I am an administrator
	When I add a new snack with name "", description "Potato chips", and price "" 
	Then the response status code should be 400

Scenario: Duplicate snack creation
	Given I am an administrator
	And there is already a snack with name "Chips" in the snack list
	When I add a new snack with name "Chips", description "Tortilla chips", and price "189"
	Then the response status code should be 400

Scenario: Successful snack deletion
	Given I am an administrator
	And there is a snack with ID "1" in the snack list
	When I delete the snack with ID "1"
	Then the response status code should be 200
	And the snack with ID "1" should no longer appear in the snack list

Scenario: Non-existent snack deletion
	Given I am an administrator
	And there is no snack with ID "9" in the snack list
	When I delete the snack with ID "9"
	Then the response status code should be 404


