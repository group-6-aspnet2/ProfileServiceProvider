# ProfileServiceProvider

## Overview
ProfileServiceProvider is an ASP.NET Core Web API for managing user profile data, such as first name, last name, address, postal code, and role. It supports profile creation both via HTTP endpoints and through integration with Azure Service Bus.

## Technologies
- ASP.NET Core Web API  
- Entity Framework Core  
- xUnit and Moq (for unit testing)  
- Swagger (OpenAPI documentation)  
- Azure Service Bus (for background processing)

## How to Run
1. Open the solution in **Visual Studio**.  
2. Set the `Presentation` project as the **Startup Project**.  
3. Press **F5** or run the project to start the API.  
4. Swagger UI is available at:  
   `https://localhost:{port}/swagger`

## How to Run Tests
1. Right-click the `Business.Tests` project.  
2. Select **Run Tests** or use the **Test Explorer**.

## API Example
- `GET /api/profile/{userId}` – Returns the profile for the specified user.

---

## Sequence Diagram – Create Profile via Service Bus
![image](https://github.com/user-attachments/assets/8532fd91-e13f-406b-a986-639d2afc4b64)

