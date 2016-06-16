Feature: Dashboard
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@GET
	Scenario: Get All Browsers
	Given the service address '/Dashboard/GetAllBrowsers'
	And the GET request
	When the request is sent
	Then the response status is OK
	And the content matches the existing golden copy

@GET
	Scenario: Get Browser Search Details
	Given the service address '/Dashboard/GetBrowserSearchDetails/7'
	And the GET request
	When the request is sent
	Then the response status is OK
	And the content matches the existing golden copy

@POST
	Scenario: Update Browser without authentication
	Given the service address '/Dashboard/Update'
	And the POST request
	When the request is sent
	Then the response status is 401 Not Authenticated


