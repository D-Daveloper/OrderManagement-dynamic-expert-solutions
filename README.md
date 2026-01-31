# Order Management API

This project is a simple Order Management API built with ASP.NET Core (.NET 9) as part of a take-home assessment.

The focus of the solution is on clarity, maintainability, and correct enforcement of business rules rather than adding unnecessary complexity.

---

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger (OpenAPI)

---

## Design Decisions

### Layered structure
The project is organised into Controllers, Services, Domain, and Infrastructure layers.

- Controllers are responsible only for HTTP concerns (routing, status codes).
- Services contain all business logic and rule enforcement.
- Domain contains the core entities.
- Infrastructure handles persistence using EF Core.

This keeps responsibilities clear and makes the code easier to reason about and extend.

---

### Business rules in services
Business rules such as preventing updates or deletions on completed orders are enforced in the service layer, not in controllers.

This avoids duplication and ensures the rules remain consistent across the application.

---

### DTOs for API contracts
DTOs are used for request payloads instead of exposing domain entities directly.

This keeps the API contract explicit and prevents tight coupling between the API and persistence models.

---

### Payments as a child resource of orders
Payments are modelled as a child resource of orders and are created and queried under an order context.

A top-level payments endpoint is also provided for listing all payments, which could support reporting or admin use cases.

---

### SQLite for persistence
SQLite was chosen over in-memory storage to better reflect a real-world setup while keeping configuration simple.

---

## Assumptions Made

- A single payment is sufficient to complete an order.
- Partial or multiple payments are not supported.
- Once a payment is created, the order is immediately marked as completed.
- Authentication and authorization are out of scope.
- No concurrency handling is required.
- Validation is kept minimal for simplicity.

---

## What I Would Improve With More Time

- Add request validation using FluentValidation.
- Add global exception handling instead of try/catch in controllers.
- Add unit and integration tests, especially around business rules.
- Add pagination for listing orders and payments.
- Add logging and basic observability.
- Introduce authentication and authorization for a production environment.

---

## Running the Project

```bash
dotnet restore
dotnet ef database update
dotnet run
