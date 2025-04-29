# To‑Do Assignment

A full‑stack to‑do list application with:
- Front‑end: React + Vite + Auth0  
- Back‑end: ASP.NET Core Web API + EF Core

---

## Prerequisites

- Node.js ≥18 (npm or yarn)  
- .NET 9 SDK  
- SQL Server (local or remote)  
- Auth0 account (domain & clientId)

---

## Front‑end Setup

```bash
cd front‑end
npm install
# configure Auth0 in src/main.jsx
npm run dev
```

Open http://localhost:5173

---

## API Setup

```bash
cd todo-api/src/TodoApi.API
# update ConnectionStrings:DefaultConnection in appsettings.json
dotnet ef database update
dotnet run
```

Swagger UI (Development): https://localhost:5001/swagger/index.html

---

## Running Tests

```bash
cd todo-api/src/TodoApi.Tests
dotnet test
```

---

## Project Structure

```
/front‑end             # React + Vite app
/todo-api
  /src
    /TodoApi.API       # ASP.NET Core Web API
    /TodoApi.Application
    /TodoApi.Domain
    /TodoApi.Infrastructure
    /TodoApi.Tests     # Unit tests (xUnit + Moq)
```

---
