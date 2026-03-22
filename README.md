# URL Shortener Service

A full-stack link management platform. This project implements a secure, scalable URL shortening system using a mathematical Base62 encoding algorithm, integrated with a modern React frontend via the Backend-for-Frontend (BFF) architecture pattern.

## Features

- **Base62 Link Shortening:** Efficiently converts relational database IDs into unique, alphanumeric short-codes.
- **Security-Hardened Authentication:** Utilizes ASP.NET Core Identity with Secure, HttpOnly cookies, completely eliminating client-side token storage vulnerabilities.
- **CSRF Mitigation:** Integrated Anti-forgery token generation and validation for all state-mutating requests.
- **Traffic Control:** Native ASP.NET Core Fixed-Window Rate Limiting protects core endpoints against DDoS attacks and spam.
- **Advanced UI Validation:** Dynamic, real-time client-side validation using React Hook Form and Zod, featuring an interactive password requirements checklist during Registration.
- **Monolithic Deployment (BFF):** Seamless single-domain operation, serving the React SPA directly from the ASP.NET Core backend to eliminate CORS overhead and simplify hosting.
- **Comprehensive Routing:** Includes public pages (About), authentication flows (Login, Registration), protected application routes, and restricted administrative Razor Pages.
- **Automated Lifecycle:** Self-configuring database with automatic Entity Framework migrations and programmatic seeding on application startup.

## Technology Stack

- **Backend:** C#, .NET 8, ASP.NET Core Web API, Razor Pages, Entity Framework Core, SQL Server, ASP.NET Core Identity
- **Testing:** xUnit, Moq, FluentAssertions
- **Frontend:** React, TypeScript, Vite, Redux Toolkit, React Hook Form, Zod, Axios, Bootstrap 

## Architecture Overview

The system architecture prioritizes security, performance, and the Single Responsibility Principle (SRP):

1. **Shortening Logic:** The service utilizes a Base62 algorithm. Upon saving an `OriginalUrl`, the system retrieves the generated ID from the database and maps it to a unique character set `[a-z, A-Z, 0-9]`.
2. **Global Error Handling:** Implements the modern .NET 8 `IExceptionHandler` combined with RFC 7807 `ProblemDetails`. This ensures predictable, standardized API error responses and keeps controllers strictly focused on routing without redundant `try-catch` blocks.
3. **Authentication Flow:** The application relies on Cookie-based Identity authentication. Anti-forgery tokens are seamlessly initialized on the React client upon load and automatically rotated upon authentication state changes.
4. **Service Decoupling:** Dependencies and middleware pipelines are managed via specialized extension methods (`AddCoreServices`, `AddAuthServices`, `AddSecurityServices`), keeping the `Program.cs` entry point strictly structured.

## How to Run

### 1. Preparing the Frontend (BFF Setup)
1. Open the frontend directory in your terminal.
2. Install dependencies:
   ```bash
   npm install
3. Build the production-ready static files:
   npm run build
4. Copy the entire contents of the generated dist folder into the wwwroot directory of the .NET backend project.

### 2. Backend API & Database
1. Open the backend project directory in your terminal.
2. Verify the DefaultConnection string in appsettings.json:
"Server=(localdb)\\mssqllocaldb;Database=UrlShortenerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
3. The database is self-initializing. Build and run the .NET project.
4. The backend will automatically apply migrations, seed the initial data, and serve the React frontend on a single port.
5. Access the application via the URL provided by the .NET launch profile (e.g., https://localhost:7076).

## Test Accounts
The following accounts are automatically seeded for evaluation purposes:
1. Username: Admin | Password: admin123 | Role: Admin
2. Username: Ivan | Password: qwerty | Role: User
3. Username: Maksym | Password: qwerty | Role: User