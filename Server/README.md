# Project Structure

This solution follows a **Clean Architecture–inspired** design with clear separation of concerns.  
Each project has a distinct purpose and dependency direction, making the codebase maintainable, testable, and adaptable.

---

## Solution Layout

**Application.API** → Web API controllers, request/response DTOs

**Application.GUI** → Future MAUI app (UI layer)

**Business.Core** → Entities, application services, use cases, direct DbContext usage  

**Business.Infrastructure** → EF Core DbContext, DbSet definitions, and shared DbSet extension methods  

**Business.Tests** → Unit & integration tests  

**Common.Shared** → Shared logic such as constants, enums, helpers, and framework-agnostic utilities  

---

## Project Responsibilities

### `Application.API`
- ASP.NET Core Web API layer.
- Handles HTTP requests and responses.
- Directly invokes **Business.Core** services and use cases.
- Maps domain entities to **DTOs** before returning results.
- **References:**
  - **Business.Core** (to execute business logic and workflows).

---

### `Application.GUI`
- Placeholder for a future **.NET MAUI** or other UI project.
- Provides a cross-platform GUI.
- Can either consume **Core services directly** or communicate through the API.
- Can be omitted until the UI is needed.

---

### `Business.Core`
- Central project combining **domain logic** (entities, rules) and **application logic** (use cases, workflows).  
- Contains:
  - **Entities and Value Objects** – core domain objects and their data.  
  - **Core business logic** – methods and rules defining entity behavior and interactions.  
  - **Domain Services** – logic that spans multiple entities.  
  - **Application Services / Use Cases** – orchestrate operations across entities.  
- **Directly injects `DbContext` or a DbContext interface** from Infrastructure to perform data access.  
- Uses **DbSet extension methods** to compose reusable and chainable queries.
    - These will return an `IQueryable` to ensure queries can be customised and chained as needed prior to execution and loading the data into memory.
- **No repository pattern**: EF Core `DbSet` + LINQ handles CRUD and query operations.  
- **References:** None (pure core logic; no dependency on frameworks or infrastructure except DbContext interface).

---

### `Business.Infrastructure`
- Contains framework-specific implementations and data access logic.  
- Examples:
  - EF Core `DbContext` implementation  
  - `DbSet<TEntity>` definitions for all entities  
  - Shared **DbSet extension methods** returning `IQueryable` for reusable queries  
  - EF Core configuration and migrations  
  - Other external services (email, caching, API clients, etc.)  
- **References:**  
  - **Business.Core** (for entities and interfaces if needed)

---

### `Business.Tests`
- Contains automated tests:  
  - Unit tests targeting **Business.Core**  
  - Integration tests targeting **Business.Infrastructure** and **Application.API**  
- Utilises **xUnit** as a testing framework.  

---

### `Common.Shared`
- Contains shared logic and utilities used across multiple layers.
- Examples:
    - Constants, enums, configuration keys
    - Generic helper methods (string, date/time utilities)
    - Shared exceptions or error types
    - Extension methods not specific to a single entity or aggregate
- Should be framework-agnostic — no EF Core, no ASP.NET Core references.
- **References**: 
None (used by any layer that needs it, but itself doesn’t reference other layers)

---

## Flow of Dependencies

At runtime, the flow typically looks like this:

1. **Application.API**  
    Receives HTTP request → invokes **Business.Core** services.

2. **Business.Core**  
    Orchestrates use case → validates → manipulates entities → queries/updates DB **directly via DbContext and DbSet extension methods**.

3. **Business.Infrastructure**  
    Provides `DbContext` implementation, `DbSet` definitions, and shared **DbSet extension methods**.

4. **Common.Shared**
    Provides constants, helpers, and shared utilities used by any layer.

---

## Dependency Rule

All dependencies **point inward**:

- `Application.API` → `Business.Core`
- `Application.API` → `Business.Infrastructure`
- `Business.Infrastructure` → `Business.Core`
- `Business.Tests` → All layers (for testing)
- `Common.Shared` → referenced by any layer as needed

This ensures **Business.Core** remains independent of frameworks and external concerns, while still allowing flexible, reusable queries through **DbSet extension methods**.