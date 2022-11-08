﻿Feature: NorthwuindCurdRequests

Checking the base requests for Northwind DB

Scenario: 1 Showing the record from the table
	When the user chooses the table
	Then the user sees data in the table

Scenario: 2 Creating the record in the table
	When the user adds new data in table
	| CategoryName | Description               |
	| Craft Drinks | Wine, Gorilka, Samogon    |
	| Craft Beer   | Goblin, Cat, Chernihivske |
	Then data is presented in table