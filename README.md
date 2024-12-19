# Production Storage API

## Overview

This service is designed for managing the leasing of areas in production facilities to host process equipment. It enables the administration of contracts for the placement of equipment, ensuring seamless coordination between facility owners and equipment renters.

## Tech Stack

- Azure App Service: A web application hosted on the Azure App Service platform.
- Azure SQL: A database hosted on Azure SQL Database.
- .NET 8.0: The application is built using ASP.NET Core, leveraging modern C# features and practices.
- Entity Framework Core: ORM used for database interactions.
- AutoMapper: Used for mapping between entities and DTOs (Data Transfer Objects).
- FluentValidator: Used for validating objects with custom rules in readable syntax.
- Swagger: API documentation and testing tool.
- xUnit: Unit testing framework.

## Features

- Get Contracts: Allows users to get the list of contracts.
- Create Contract Details: User can create a new placement contract.
- Get Security Key: To gain access to the endpoints.

## Setup Instructions

### Remotely

https://storage-service-fcezg3hmapfvaffm.canadacentral-01.azurewebsites.net/swagger/index.html

### Locally

### 1. Clone the repository to your local machine

```cmd
git clone https://github.com/me1ncun/Production-Storage-Service.git
```

### 2. Set Up the Database

- Modify the appsettings.json file to include your database connection string:

```json
"ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Initial Catalog=ProductionStorage;Integrated Security=true;TrustServerCertificate=True"
}
```

### 3. Migrate the Database

After setting up your connection string, the database will automatically update when the application starts. You can also run the following command manually to apply any pending migrations:

```cmd
dotnet ef database update
```

### 4. Run the Application

To run the API locally, use the following command:

```cmd
dotnet run
```

### 5. Testing the Endpoints

Once the application is running, you can test the endpoints using Swagger UI.

- Swagger UI: Navigate to https://localhost:7171/index.html for a visual interface to test the API.

### 6. Tests

Unit tests for the controller are written using xUnit and Moq. These tests can be run with the following command:

```cmd
dotnet test
```
