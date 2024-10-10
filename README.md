This application has been created for CRUD operation using below technologies.

1. Angular 18
2. .NET 8 API
3. SQL Server
4. Entity Framework Core

How to run the application locally?
Goto "DatabaseChanges" folder and run all the scripts within it. This will create initial buildings needed to run the app.
Also run another sql file "SP_InsertUpdateCustomer.SQL" file.

Open the CRM Service folder and open the .sln file to open the API locally, then run as usual by F5.

Open the CRMUIService->src folder in VS Code tool and run "ng serve" command to launch UI service locally.

How to authenticate the app 
Use users table credentials, i have attached sql file which has (admin as user id and Test#123 as password)

How to test the API.

1. Invoke the Authentication API with username and password above mentioned and get the JWT token.
2. Pass this token to invoke customers API methods as Bearer token.

