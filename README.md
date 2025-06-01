# ProfileServiceProvider

## Overview
ProfileServiceProvider is an ASP.NET Core Web API for managing user profile data, including first name, last name, address, postal code, and role.

## Technologies
- ASP.NET Core Web API  
- Entity Framework Core  
- xUnit and Moq (unit testing)  
- Swagger (OpenAPI documentation)

## How to Run
1. Open the solution in Visual Studio.  
2. Set the `Presentation` project as the Startup Project.  
3. Press **F5** or run the project.  
4. Swagger UI will be available at:  
   `https://localhost:{port}/swagger`

## How to Run Tests
1. Right-click the `Business.Tests` project.  
2. Select **Run Tests** or use the Test Explorer.

## API Example
**GET** `/api/profile/{userId}`  
Returns the profile for the specified user.

![image](https://github.com/user-attachments/assets/8dff1ba7-6faf-49e3-a8af-af709e867c07)


