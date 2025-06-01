ProfileServiceProvider
🧾 Overview
ProfileServiceProvider is an ASP.NET Core Web API that handles user profile management, including fields such as first name, last name, address, postal code, and role.

⚙️ Technologies Used
ASP.NET Core Web API

Entity Framework Core

xUnit and Moq (unit testing)

Swagger (API documentation)

▶️ How to Run the API
Open the solution in Visual Studio.

Set the Presentation project as the Startup Project.

Press F5 or use dotnet run to launch the API.

Open Swagger UI at:
https://localhost:{port}/swagger

🧪 Running Tests
Right-click on the Business.Tests project.

Choose Run Tests or use the Test Explorer.

📌 API Example
GET /api/profile/{userId}
Returns the profile for the specified user.
