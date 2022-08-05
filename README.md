[![CodeQL](https://github.com/msx752/sample-netcore-api/actions/workflows/codeql-analysis.yml/badge.svg?branch=master)](https://github.com/msx752/sample-netcore-api/actions/workflows/codeql-analysis.yml)

## sample-net6-api
Sample Online Movie Store Website built on microservice api architecture with AngularUI using Ocelot Gateway

### Project Contents
- Microservice Architecture 
- Messagging Protocol via MassTransit
  - Pipeline between microservices
- Angular Website Application
  - Base API Client
  - API Response Error Handling
  - Login via Bearer Token
  - Session Managing
- Ocelot Gateway
- Docker Compose File
- Docker Containers
  - Cart API
  - Ocelot Gateway
  - Identity API
  - Movie API
  - Payment API
  - Angular WebApplication
- RESTful API on Controller
- Contract Library
  - Messaging Models
- Core Library
  - Centralized Startup Configurations 
- Global Exception Handler
- Logging API Request / Response ( **NOT COMPLETED** )
  - MongoDb Logging ( **NOT COMPLETED** )
- Single API Response Model
  - Request Tracking Id
  - Measuring Response Time
- OAtuh2 Authorization (JWT Token)
- XUnit Integration Test Project
- Configuration
  - Shared AppSetting Structure
  - Environment Based Appsettings Configuration
- Middlewares
  - JWTMiddleware
  - ExceptionMiddleware
- AutoMapper
  - Dto
  - RequestModel
  - EntityModel
- EntityFrameworkCore
  - BaseEntity
  - Auditlog to SqlTable
- CustomizedDbContext
  - DbContextSeed ( **REQUIRE IMPROVEMENT / NOT COMPLETED** )
- Repository Pattern
  - UnitOfWork
- Dependency Injections
- Swagger
- RabbitMQ
- In Memory Database

**will continue to include other features**
