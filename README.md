# Product Management System

A simple **ASP.NET Core Web API** project for managing products using **CQRS, MediatR, AutoMapper, Entity Framework Core, and API Versioning**.

The project was developed to understand how a traditional CRUD application can be structured using **CQRS**, and how **MediatR** can be used to separate API requests from their actual business operations.

---

## 📌 Project Overview

The Product Management System provides APIs to perform basic product operations:

* Create a product
* Get all products
* Get a product by ID
* Update a product
* Delete a product

The project contains two approaches:

1. **Manual CQRS implementation**
2. **CQRS implementation using MediatR**

The original manual implementation is kept in the project so that both approaches can be understood and compared.

---

## 🛠️ Technologies Used

* **C#**
* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **CQRS Pattern**
* **MediatR**
* **AutoMapper**
* **API Versioning**
* **Swagger / OpenAPI**
* **Git & GitHub**

---

# 🏗️ Project Structure

```text
ProductManagementSystem
│
├── ProductManagementSystem.Api
│   │
│   ├── Endpoints
│   │   ├── ProductEndpoints.cs
│   │   └── MediatRProductEndpoints.cs
│   │
│   └── Program.cs
│
├── ProductManagementSystem.Service
│   │
│   ├── Commands
│   │   └── Products
│   │       ├── CreateProduct
│   │       ├── UpdateProduct
│   │       └── DeleteProduct
│   │
│   ├── Queries
│   │   └── Products
│   │       ├── GetProducts
│   │       └── GetProductById
│   │
│   ├── DTOs
│   │   ├── ProductDto.cs
│   │   └── ProductV2Dto.cs
│   │
│   ├── Mappings
│   │   └── ProductMappingProfile.cs
│   │
│   └── MediatR
│       │
│       ├── Commands
│       │   └── Products
│       │       ├── CreateProduct
│       │       ├── UpdateProduct
│       │       └── DeleteProduct
│       │
│       └── Queries
│           └── Products
│               ├── GetProducts
│               └── GetProductsV2
│
└── ProductManagementSystem.Core
    │
    ├── Domain
    │   └── Entities
    │       └── Product.cs
    │
    └── ApplicationBbContext
        └── AppDbContext.cs
```

---

# 🔄 Application Flow

The application follows a simple layered structure:

```text
Client
   │
   ▼
API Endpoint
   │
   ▼
Service Layer
   │
   ▼
CQRS Command / Query
   │
   ▼
Handler
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

For the MediatR implementation:

```text
Client
   │
   ▼
API Endpoint
   │
   ▼
IMediator
   │
   ▼
Request
(Command / Query)
   │
   ▼
Request Handler
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

---

# 📚 CQRS Pattern

## What is CQRS?

**CQRS** stands for:

> Command Query Responsibility Segregation

The main idea is to separate operations that **change data** from operations that **read data**.

There are two types of operations:

### Commands

Commands are used when we want to change data.

Examples:

```text
Create Product
Update Product
Delete Product
```

### Queries

Queries are used when we only want to read data.

Examples:

```text
Get All Products
Get Product By ID
```

---

## CQRS Flow

```text
                 Product API
                     │
          ┌──────────┴──────────┐
          │                     │
       COMMAND                QUERY
          │                     │
    Create/Update/Delete     Get Products
          │                     │
          ▼                     ▼
      Command Handler      Query Handler
          │                     │
          └──────────┬──────────┘
                     │
                     ▼
                EF Core
                     │
                     ▼
                 Database
```

This separation makes the responsibility of each operation easier to understand.

---

# 🧩 Manual CQRS Implementation

Before using MediatR, CQRS was implemented manually.

The flow is:

```text
API Endpoint
     │
     ▼
IHandler
     │
     ▼
Handler
     │
     ▼
EF Core
     │
     ▼
SQL Server
```

For example:

```text
POST /api/products
       │
       ▼
CreateProductHandler
       │
       ▼
AppDbContext
       │
       ▼
Products Table
```

This approach helps demonstrate how CQRS works internally without depending on a mediator library.

---

# 🚀 MediatR

## What is MediatR?

**MediatR** is a library that helps implement the **Mediator Pattern**.

Instead of an API endpoint directly calling a specific handler, the endpoint sends a request through `IMediator`.

The mediator finds the appropriate handler and executes it.

---

## MediatR Flow

```text
API Endpoint
     │
     ▼
IMediator
     │
     ▼
Request
     │
     ▼
Request Handler
     │
     ▼
EF Core
     │
     ▼
SQL Server
```

For example:

```text
POST /api/mediator/products
          │
          ▼
CreateProductMediatRCommand
          │
          ▼
IMediator.Send()
          │
          ▼
CreateProductMediatRCommandHandler
          │
          ▼
AppDbContext
          │
          ▼
SQL Server
```

---

# 🔀 Why Use MediatR?

Without MediatR:

```text
Endpoint
   │
   ▼
Handler
```

With MediatR:

```text
Endpoint
   │
   ▼
IMediator
   │
   ▼
Handler
```

The API endpoint does not need to know which handler is responsible for processing the request.

This helps reduce direct dependencies between components and makes the application easier to organize as the number of commands and queries grows.

---

# 🌐 API Versioning

API versioning allows different versions of an API to exist at the same time.

For example:

```text
/api/v1/products
/api/v2/products
```

This is useful when an API changes but existing clients still need the older version.

---

## Version 1

The first version returns the original product response.

```text
GET /api/v1/products
```

Flow:

```text
GET /api/v1/products
        │
        ▼
GetProductsMediatRQuery
        │
        ▼
GetProductsMediatRQueryHandler
        │
        ▼
Database
        │
        ▼
ProductDto
```

---

## Version 2

A second version was introduced to demonstrate how the API can evolve.

```text
GET /api/v2/products
```

The V2 endpoint uses a separate query and DTO.

```text
GET /api/v2/products
        │
        ▼
GetProductsV2Query
        │
        ▼
GetProductsV2QueryHandler
        │
        ▼
Database
        │
        ▼
ProductV2Dto
```

This allows V1 and V2 to have different response structures without breaking existing clients.

---

# 🔗 API Endpoints

## Manual CRUD APIs

| Method | Endpoint             | Purpose           |
| ------ | -------------------- | ----------------- |
| POST   | `/api/products`      | Create product    |
| GET    | `/api/products`      | Get all products  |
| GET    | `/api/products/{id}` | Get product by ID |
| PUT    | `/api/products/{id}` | Update product    |
| DELETE | `/api/products/{id}` | Delete product    |

---

## MediatR APIs

| Method | Endpoint           | Purpose                     |
| ------ | ------------------ | --------------------------- |
| GET    | `/api/v1/products` | Get products using MediatR  |
| GET    | `/api/v2/products` | Get products using V2 query |

Additional MediatR CRUD endpoints are also implemented separately from the original manual CRUD implementation.

---

# 🗂️ Main Project Responsibilities

## ProductManagementSystem.Api

Responsible for:

* API endpoints
* HTTP requests and responses
* API versioning
* Dependency injection
* Application startup configuration

---

## ProductManagementSystem.Service

Responsible for:

* Business/application operations
* Commands
* Queries
* Handlers
* DTOs
* AutoMapper profiles
* MediatR requests and handlers

---

## ProductManagementSystem.Core

Responsible for:

* Domain entities
* Database context
* Core application/data definitions

---

# 🧱 Manual CQRS vs MediatR

| Manual CQRS                     | MediatR CQRS                            |
| ------------------------------- | --------------------------------------- |
| Endpoint calls handler directly | Endpoint sends request through mediator |
| Custom handler interfaces       | `IRequest` / `IRequestHandler`          |
| More manual wiring              | Less direct coupling                    |
| Good for understanding CQRS     | Good for larger applications            |
| No external mediator library    | Uses MediatR                            |

Both implementations are kept in this project for learning and comparison.

---

# 🔄 Complete Project Flow

The overall application can be understood as:

```text
                         CLIENT
                           │
                           ▼
                    API ENDPOINT
                           │
              ┌────────────┴────────────┐
              │                         │
              ▼                         ▼
        Manual CQRS                 MediatR
              │                         │
              ▼                         ▼
          Handler                  IMediator
                                        │
                                        ▼
                                   Command/Query
                                        │
                                        ▼
                                     Handler
              │                         │
              └────────────┬────────────┘
                           │
                           ▼
                       EF CORE
                           │
                           ▼
                       SQL SERVER
                           │
                           ▼
                        RESPONSE
```

---

# 🎯 What I Learned From This Project

This project was created to understand the practical implementation of:

* ASP.NET Core Web API
* CRUD operations
* Layered architecture
* CQRS pattern
* Commands and Queries
* Manual CQRS implementation
* MediatR
* Request and response handling
* API versioning
* DTOs
* AutoMapper
* Entity Framework Core
* Dependency Injection
* Swagger API testing
* Git and GitHub

---

# ⚙️ Getting Started

## Prerequisites

Make sure the following are installed:

* .NET 8 SDK
* Visual Studio or Visual Studio Code
* SQL Server
* Git

---

## Clone the Repository

```bash
git clone <your-repository-url>
```

Navigate to the project:

```bash
cd ProductManagementSystem
```

---

## Configure Database

Update the database connection string in the application's configuration according to your local SQL Server environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-sql-server-connection-string"
  }
}
```

> Do not commit real passwords, secrets, API keys, or other sensitive configuration values to GitHub.

---

## Run the Application

From the API project directory:

```bash
dotnet run
```

The API can then be tested using **Swagger UI**.

---

# 🧪 Testing

Swagger can be used to test the API endpoints.

The main operations that can be tested are:

```text
Create Product
Get Products
Get Product By ID
Update Product
Delete Product
```

API versioning can also be tested using:

```text
/api/v1/products
/api/v2/products
```

---

# 📌 Key Concepts

### CQRS

Separates **commands** that change data from **queries** that read data.

### MediatR

Acts as a mediator between the API endpoint and the request handler.

### API Versioning

Allows multiple API versions to exist without immediately breaking existing clients.

### DTO

A Data Transfer Object controls the data that is sent between the API and client.

### Entity Framework Core

Provides the application's database access layer and communicates with SQL Server.

### AutoMapper

Maps objects between different models, such as entities and DTOs.

---

# 🚧 Future Improvements

The project can be extended with:

* Authentication and Authorization
* JWT / Okta authentication
* Repository Pattern
* Unit Testing
* Integration Testing
* Global Exception Handling
* Validation
* Logging
* FluentValidation
* MediatR Pipeline Behaviors
* Pagination
* Filtering and Sorting
* Docker
* CI/CD
* Microservices architecture

---

# 👨‍💻 Author

**Tangudu Raja**

B.Tech - Computer Science

This project was created as a practical learning project to understand **ASP.NET Core, CQRS, MediatR, API Versioning, and clean application structure**.

---


