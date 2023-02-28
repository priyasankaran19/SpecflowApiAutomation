Feature: UsersApi

A short summary of the feature

@usersApi
Scenario: Get Single User Request - for valid user

	Given "GET" request
	And Pass the userId "2" in the request for "User" endpoint
	Then The response status should be "200"
	Then The "user" reponse record should be:
		| id | email                  | first_name | last_name | avatar                                  |
		| 2  | janet.weaver@reqres.in | Janet      | Weaver    | https://reqres.in/img/faces/2-image.jpg |

Scenario: Get Single User Request - for invalid user

	Given "GET" request
	And Pass the userId "1234567890" in the request for "User" endpoint
	Then The response status should be "404"


Scenario: Get Single Resource - for valid input

	Given "GET" request
	And Pass the resourceId "2" in the request for "Resource" endpoint
	Then The response status should be "200"
	Then The "resource" reponse record should be:
		| id | name         | year  | color		|  pantone_value |
		| 2  | fuchsia rose | 2001  | #C74375	| 17-2031		 |

Scenario: Get Single Resource - for invalid user

	Given "GET" request
	And Pass the resourceId "0123456789" in the request for "Resource" endpoint
	Then The response status should be "404"


Scenario: Get Users List
	Given "GET" request
	And Pass the "page" param as "2" in the request  for "User" endpoint
	Then The response status should be "200"

Scenario: Get Users List with Delay param
	Given "GET" request
	And Pass the "delay" param as "3" in the request  for "User" endpoint
	Then The response status should be "200"

Scenario: Get Resource List
	Given "GET" request
	And Pass the resourceId "" in the request for "Resource" endpoint
	Then The response status should be "200"	
	
Scenario: Post Create User

	Given "POST" request
	And Pass the json input file as payload for "User" endpoint
		| FileName |
		| TestData\createUser.json   |
	Then The response status should be "201"

Scenario: Put Update User

	Given "PUT" request
	And Pass the json input file as payload for "User" endpoint for userId "2" in the request
		| FileName |
		| TestData\updateUser.json   |
	Then The response status should be "200"

Scenario: DELETE User

	Given "DELETE" request
	And Pass the userId "2" in the request for "User" endpoint
	Then The response status should be "204"


Scenario: Post Login Valid Request
	Given "POST" request
	And Pass the json input file as payload for "Login" endpoint
		| FileName |
		| TestData\login.json   |
	Then The response status should be "200"

Scenario: Post Login In Valid Request
	Given "POST" request
	And Pass the json input file as payload for "Login" endpoint
		| FileName |
		| TestData\login-invalid.json   |
	Then The response status should be "400"

Scenario: Post Register Valid Request
	Given "POST" request
	And Pass the json input file as payload for "Register" endpoint
		| FileName |
		| TestData\register.json   |
	Then The response status should be "200"

Scenario: Post Register In Valid Request
	Given "POST" request
	And Pass the json input file as payload for "Register" endpoint
		| FileName |
		| TestData\register-invalid.json   |
	Then The response status should be "400"		