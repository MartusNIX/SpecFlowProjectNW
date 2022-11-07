Feature: NorthwuindCurdRequests

Checling the base request

Scenario: 1 Showing the record from the table
	When the user chooses the table
	Then the user sees data in the table

	Scenario: 2 Creating the record in the table
	When the user adds new data
	Then data is presented in table