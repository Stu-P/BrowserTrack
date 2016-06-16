Feature: Contact
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@POST
	Scenario: Attempt enquiry without authentication
	Given the service address '/Contact/Enquiry'
	And the POST request
	And the request body parameter "{"firstName":"stu","lastName":"test","email":"stutest@test.tst","phone":"454258464","enquiry":"hi this is a test enquiry"}"
	When the request is sent
	Then the response status is 401 Not Authenticated


	@POST
	Scenario: Attempt enquiry after successful authentication
	Given the service address '/Contact/Enquiry'
	And the POST request
	And successful bearer token authentication with user "stutest" and password "Password12"
	And the request body parameter "{"firstName":"stu","lastName":"test","email":"stutest@test.tst","phone":"454258464","enquiry":"hi this is a test enquiry"}"
	When the request is sent
	Then the response status is OK
