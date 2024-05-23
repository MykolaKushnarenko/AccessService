# AccessService
This exercise is a test assigment.

Note: Since there are only 3 hours, the project was implemented in a rush.

Things left to improve:
* Validation
* Global exception
* Test coverage
* Code documentation
* Solution structure

# This project includes 4 APIs:
- Create API key 
- Use API key
- Revoke API key
- Get tokens

Swagger contains description and examples how to call APIs.

Stack:
- .NET 8
- MSSQL
- EF Core
- Mediatr

Dependency:
Before runing the project ensure DB is up and running. Once DB is set, apply migrations.
In order to do this set Database:ConnectionStringSecret in appsettings.local.json

Test user id: 