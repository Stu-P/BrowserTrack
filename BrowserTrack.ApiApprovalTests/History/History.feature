Feature: History
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


	Scenario: Get All Version Changes
	Given the service address '/History/GetAllVersionChanges'
	And the GET request
	When the request is sent
	Then the response status is OK
	And the content matches the existing golden copy