 # EduGuide System Architecture

## Overview

The EduGuide system is designed to facilitate educational counseling by connecting students with academic advisors based on their educational needs. The platform enables students to receive personalized guidance while allowing advisors to effectively manage and support their assigned students.

The system provides a centralized platform where administrators can assign academic advisors to students. Once assigned, students and advisors can communicate through a real-time online chat, enabling continuous guidance, discussion of academic progress, and timely support throughout the counseling process.

From an architectural perspective, the project demonstrates the application of Clean Architecture together with CQRS, MediatR, the State Pattern, Optimistic Concurrency Control, and real-time communication using SignalR.

---

# Architectural Goals

The architecture was designed to satisfy both functional and non-functional requirements of the system. The primary objectives are described below.

- **Separation of Concerns:** Each layer is responsible for a single aspect of the application, reducing complexity and making the codebase easier to understand.

- **Maintainability:** The architecture allows individual components to evolve independently with minimal impact on other layers.

- **Low Coupling:** Business logic depends on abstractions rather than concrete implementations, improving flexibility and enabling easier replacement of infrastructure components.

- **Reusability:** Shared functionality is extracted into reusable Base libraries, allowing common services and utilities to be reused across multiple projects.

- **Scalability:** The layered design and clear separation between read and write operations allow the application to grow without introducing excessive complexity.

- **Testability:** Business logic is isolated from external dependencies, making unit testing straightforward.

- **Consistency:** Validation, exception handling, authentication, and other cross-cutting concerns are centralized, ensuring consistent behavior throughout the application.

- **Concurrency Safety:** The system detects conflicting updates using optimistic concurrency control, preventing data inconsistency caused by simultaneous operations.

# Solution Structure

The project follows Clean Architecture.

```text
Base
├── Base.Api
├── Base.Application
├── Base.Application.Contracts
├── Base.Domain
├── Base.Infrastructure
└── Base.Utilities

Gateway
└── EduGuide.Gateway

Modules
└── EduGuide
    ├── EduGuide.Application
    ├── EduGuide.Domain
    └── EduGuide.Infrastructure
```

---

# Layer Responsibilities

## API

Responsible for:

- REST endpoints
- Dependency Injection
- Authentication configuration
- Authorization
- Swagger
- SignalR configuration

---

## Application

Responsible for:

- CQRS
- MediatR handlers
- Business workflows
- Validation
- DTOs

---

## Domain

Responsible for:

- Entities
- Business rules
- State Pattern
- Domain logic
- Enumerations

The Domain layer contains no infrastructure dependencies.

---

## Infrastructure

Responsible for:

- Entity Framework Core
- Repository implementation
- Unit of Work
- SQL Server
- JWT implementation
- MinIO integration
- SignalR services

---

# Shared Base Libraries

The solution contains reusable libraries shared across multiple applications.

## Base.Api

Provides:

- Shared middleware registration
- Swagger configuration
- Common API configuration

## Base.Application

Provides:

- Global exception handling
- Shared exceptions
- Date conversion utilities
- Common helper classes

## Base.Application.Contracts

Contains shared abstractions.

Examples include:

- IRepository
- IUnitOfWork
- IJwtManager
- IMinIoService

It also contains common DTOs used across applications.

## Base.Domain

Contains:

- BaseEntity
- Shared enums
- Common domain abstractions

## Base.Infrastructure

Provides implementations of shared contracts including:

- Generic Repository
- Unit of Work
- JWT Manager
- MinIO Service
- Base DbContext
- Soft Delete implementation

---

# Request Processing Flow

The application uses CQRS together with MediatR.

Every request follows the pipeline below.

```text
Client
    │
    ▼
Controller
    │
    ▼
MediatR
    │
    ▼
ValidationPipelineBehavior
    │
    ▼
FluentValidation
    │
    ▼
Command / Query Handler
    │
    ▼
Unit Of Work
    │
    ▼
Repository
    │
    ▼
Entity Framework Core
    │
    ▼
SQL Server
```

Validation is performed automatically through a custom ValidationPipelineBehavior before requests reach their handlers.

---

# Data Access

Data persistence is implemented using:

- SQL Server
- Entity Framework Core
- Code First
- Generic Repository Pattern
- Unit of Work Pattern

Database schema changes are managed using EF Core Migrations.

---

# Authentication & Authorization

Authentication is implemented using JWT.

After successful authentication the application generates a JWT token containing all required user claims.

Authorization is implemented using Role-Based Access Control (RBAC).

The system currently defines two roles:

- Counselor
- Student
- Admin

Protected endpoints use ASP.NET Core Authorize attributes.

---

# Real-Time Chat Architecture

The application provides a real-time private messaging system using SignalR.

Unlike traditional HTTP-based messaging, SignalR enables bidirectional communication between the server and connected clients, allowing messages to be delivered instantly without requiring page refreshes.

Messages are persisted in the database, ensuring that conversations remain available even when users are offline.

```text
User Sends Message
        │
        ▼
SignalR Hub
        │
        ▼
JWT Authentication
        │
        ▼
MessageCreateCommand
        │
        ▼
Database (Message Storage)
        │
        ▼
Check Receiver Connection
        │
   ┌────┴─────┐
   │          │
Online     Offline
   │          │
   ▼          ▼
Deliver     Stored
Message     for Later
```

---

## Connection Management

The SignalR Hub maintains active user connections using in-memory concurrent dictionaries.

The hub is responsible for:

- Tracking connected users.
- Maintaining online/offline status.
- Managing active private conversations.
- Delivering messages to connected users.

When a user connects:

- The JWT token is validated.
- The user's connection is registered.
- Contacts are notified that the user is online.
- The client receives the list of currently online contacts.

When a user disconnects:

- The connection is removed.
- Active chat information is cleared.
- Contacts are notified that the user has gone offline.

---

## Message Delivery

When a private message is sent:

1. The sender is authenticated using the JWT token.
2. The message is persisted through the Application layer using CQRS.
3. The hub checks whether the recipient is currently connected.
4. If connected, the message is delivered immediately.
5. Otherwise, the message remains stored in the database and can be retrieved later.

---

## Read Receipts

The system supports message read receipts.

When the recipient has an active conversation with the sender:

- Messages are automatically marked as seen.
- The sender is notified immediately through SignalR.

This mechanism ensures real-time synchronization of message status between both participants.

---

## File Sharing

The chat system supports file attachments.

Uploaded files are stored in MinIO object storage, while only the file reference is stored in the database.

When a file message is delivered, the application generates a temporary download URL through the MinIO service before sending it to the client.

---

## Online Presence

The hub continuously tracks user presence.

Whenever a user connects or disconnects, all related contacts are notified immediately through SignalR.

This enables the client application to display real-time online/offline status.

---
# File Storage

Binary files are stored using MinIO object storage.

Only the object identifier is stored in SQL Server.

When a file is requested:

1. The object name is retrieved from SQL Server.
2. The file is downloaded from MinIO.

This keeps the relational database free from large binary objects.

---

# Error Handling

The application provides centralized exception handling through ErrorHandlerMiddleware.

Responsibilities include:

- Handling validation errors
- Handling concurrency exceptions
- Returning consistent API responses

---

# Architectural Decisions

## Clean Architecture

Chosen to separate business logic from infrastructure concerns while improving maintainability and testability.

## CQRS + MediatR

The application adopts the Command Query Responsibility Segregation (CQRS) pattern to separate operations that modify application state from operations that only retrieve data.

Write operations are implemented as **Commands**, while read operations are implemented as **Queries**.

MediatR acts as an in-process message dispatcher that routes each request to its corresponding handler.

This design provides several architectural advantages:

- Controllers remain thin and only dispatch requests.
- Business logic is encapsulated inside dedicated handlers.
- Commands and Queries evolve independently.
- Cross-cutting concerns such as validation can be implemented once using Pipeline Behaviors instead of duplicating logic across handlers.
- New features can be added by introducing new handlers without affecting existing functionality.

By combining CQRS with MediatR, the application achieves better modularity, maintainability, and extensibility.

## Repository + Unit of Work

The infrastructure layer follows the Repository and Unit of Work patterns.

The Repository pattern abstracts data access operations behind a generic interface, allowing the Application layer to work with entities without depending directly on Entity Framework Core.

The Unit of Work coordinates all repository operations within a single business transaction. Instead of saving changes after every repository call, all modifications are accumulated and committed together.

This approach provides several advantages:

- Clear separation between business logic and persistence logic.
- Centralized transaction management.
- Reduced duplication of CRUD operations through generic repositories.
- Easier testing by depending on abstractions rather than concrete implementations.
- Consistent persistence behavior across the application.


## Optimistic Concurrency

Since multiple users may interact with the same volunteer task simultaneously, the system uses Entity Framework Core's optimistic concurrency mechanism to prevent conflicting updates.

Each protected entity contains a concurrency token that is verified during update operations.

If another user modifies the same entity before the current transaction is committed, EF Core detects the conflict and throws a `DbUpdateConcurrencyException`.

Compared to pessimistic locking, optimistic concurrency offers several benefits:

- No long-lived database locks.
- Better scalability for web applications.
- Higher throughput under normal workloads.
- Automatic conflict detection with minimal implementation complexity.

Concurrency exceptions are handled centrally through the global ErrorHandlerMiddleware, ensuring consistent API responses.

## Shared Base Libraries

Rather than placing common infrastructure code inside each project, the solution introduces a reusable set of Base libraries.

These libraries contain generic functionality that is independent of the Volunteer Task Management domain and can be reused in future applications.

Examples include:

- Generic repositories
- Unit of Work
- JWT management
- MinIO integration
- Shared DTOs
- Base entities
- Common middleware
- Exception handling
- Date conversion utilities

This architecture provides several advantages:

- Reduces code duplication.
- Encourages consistency across projects.
- Simplifies maintenance.
- Improves long-term extensibility.
- Allows domain-specific projects to focus only on business requirements.

## SignalR Real-Time Communication

SignalR was selected to provide real-time communication capabilities within the application.

The SignalR Hub is responsible for managing user connections, tracking online presence, delivering private messages, synchronizing message status, and supporting file sharing.

Unlike traditional request-response communication over HTTP, SignalR enables persistent connections between clients and the server, allowing events to be pushed immediately to connected users.

Messages are first persisted through the CQRS pipeline and then delivered to online recipients. If a recipient is offline, the message remains stored and can be retrieved when the conversation is reopened.

This architecture provides several benefits:

- Low-latency message delivery.
- Persistent message history.
- Real-time online/offline presence tracking.
- Read receipt synchronization.
- Efficient communication without continuous client polling.
- Integration with MinIO for file attachments.

## JWT + RBAC

Authentication is implemented using JSON Web Tokens (JWT).

After successful authentication, the application generates a token containing the user's identity and authorization claims.

Authorization is implemented using Role-Based Access Control (RBAC), where permissions are determined by the user's assigned role.

This design offers several advantages:

- Stateless authentication.
- No server-side session management.
- Better scalability for distributed applications.
- Clear separation of responsibilities between Coordinators and Volunteers.
- Simple integration with ASP.NET Core's built-in authorization framework.

---

# Technologies

| Category | Technology |
|----------|------------|
| Framework | ASP.NET Core |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Messaging | MediatR |
| Validation | FluentValidation |
| Authentication | JWT |
| Authorization | RBAC |
| Real-time Communication | SignalR |
| Object Storage | MinIO |
| Testing | xUnit |
