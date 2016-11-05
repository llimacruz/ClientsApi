# ClientsApi

API developed to provide clients control to an e-comerce website.
Technology adopted:
- Asp.net webapi 2 (REST services)
- Entity Framework 6 (persistence/ORM)
- Ninject (dependency injection)
- Moq (unit tests mocking framework)

Business properties:
- All clients must have a valid CPF;
- Client can have several phone numbers;
- All data is mandatory.

Extra:
In the TestPage folder, there is a webpage that allows to test the API.
The test page uses jquery ajax functions to call the REST services.
In order to accomplish this test, it was necessary install CORS (enable cross-origin) package in the project.

URL: api/clients
Services (verbs) available:

GET
returns all clients
200 - success
500 - internal error (any error reading data)

GET with cpf (api/clients/9999999)
returns the client who owns the specific cpf
200 - success
404 - client not found
500 - internal error (any error reading data)

POST / PUT
create or update the client
200/201 - PUT updated/post created client
400 - bad request - any invalid information in the client data
505 - internal error (any error saving data)

DELETE (api/clients/9999999)
delete de client who owns the specific cpf
200 - success
404 - client not found
500 - internal error (any error deleting the client)
