# EShopMicroservices
Miscroservice-oriented implementation of an E-commerce web application using ASP.NET Core.


# Useful Commands
Commands to initiate and update migrations for Ordering Microservice:
 - dotnet ef migrations add InitialCreate -o Data/Migrations -p Ordering Infrastructure -s Ordering.API
 - dotnet ef database update -p Ordering.Infrastructure -s Ordering.API
Ensure to run the commands from Ordering folder
