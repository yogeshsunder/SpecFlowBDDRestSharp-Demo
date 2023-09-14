Feature: CreateUser
Create a new user

@tag1
Scenario: Create a new user with valid inputs
	Given User payload "CreateUser.json"
	When Send request to create user
	Then Validate user is created
