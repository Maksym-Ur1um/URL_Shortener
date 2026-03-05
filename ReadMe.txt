# URL Shortener Service

A full-stack link management platform. This project implements a secure, scalable URL shortening system using a mathematical Base62 encoding algorithm on the backend, integrated with a modern React frontend and administrative Razor Pages.

## Features

- **Base62 Link Shortening:** Efficiently converts relational database IDs into unique, alphanumeric short-codes.
- **Security-Hardened Auth:** Secure JWT-based authentication utilizing HttpOnly Cookies to mitigate XSS and unauthorized token access.
- **Administrative Content Management:** Dedicated Razor Pages for content editing, restricted to users with the `Admin` role.
- **Automated Database Seeding:** Self-configuring database lifecycle with automatic migrations and programmatic seeding on application startup.
- **Cross-Platform Navigation:** Seamless integration between React SPA routing and server-side rendered Razor Pages.
- **Unit Testing Coverage:** Core business logic and Base62 encoding verified via xUnit, Moq, and FluentAssertions.

## Technology Stack

- Backend: C#, .NET, ASP.NET Core Web API, Razor Pages, Entity Framework Core, SQL Server
- Testing: xUnit, Moq, FluentAssertions
- Frontend: React, TypeScript, Redux Toolkit, Axios, Bootstrap 

## Architecture Overview

The system architecture prioritizes security and the Single Responsibility Principle (SRP):
1. Shortening Logic: The service utilizes a Base62 algorithm. Upon saving an `OriginalUrl`, the system retrieves the generated ID from the database and maps it to a unique character set `[a-z, A-Z, 0-9]`.
2. Authentication Flow: Unlike standard LocalStorage implementations, this project issues a JWT stored in a secure HttpOnly cookie, ensuring the token is inaccessible to client-side scripts.
3. Service Decoupling: Dependencies are managed via specialized extension methods (`AddCoreServices`, `AddAuthServices`), keeping the `Program.cs` entry point clean and maintainable.

## How to Run

### 1. Backend API & Razor Pages
1. Open the backend project directory in your terminal.
2. Verify the `DefaultConnection` string in `appsettings.json`:
   `"Server=(localdb)\\mssqllocaldb;Database=UrlShortenerDb;Trusted_Connection=True;MultipleActiveResultSets=true"`
3. The database is self-initializing. Build and run the project.
4. The backend will automatically apply migrations and seed the initial users and content.

### 2. Frontend Web App
1. Open the frontend directory in your terminal.
2. Install dependencies via npm:
   npm install
3. Start the local development server:
4. Access the application at http://localhost:5173. Ensure the API base URL in your Axios configuration matches your local backend address.

###Test Accounts

The following accounts are automatically seeded for evaluation:
1. Username: Admin, Password: admin123, Role: Administrator
2. Username: Ivan, Password: qwerty, Role: User
3. Username: Maksym, Password: qwerty, Role: User