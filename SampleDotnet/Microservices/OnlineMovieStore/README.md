## OnlineMovieStore
Sample Online Movie Store Website built on microservice architecture with AngularUI using Ocelot Gateway

## How to run demo on the docker linux containers
``` powershell
cd SampleDotnet/Microservices/OnlineMovieStore
docker-compose down
docker-compose build --no-cache
docker-compose up
```

## How to access
```
Website Endpoint: http//localhost:2000 (requires nodejs v14+)
  - username: user1
  - password: password1
Gateway API Endpoint: http//localhost:1010
MSSQL Server: localhost,1433
  - username: sa
  - password: Admin123!
```

```
Other services reachable over gateway service, checkout 'DockerUp.ps1' and 'docker-compose.yml' files
```

``` css
MongoDB : ports 27017-27019 (not used in the project yet)
RabbitMQ: ports 5672, 15672
MSSQL Server: port 1433
```


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
  - ~~Auditlog to SqlTable~~
  - DbInitializer Seed
- Repository Pattern
- Dependency Injections
- Swagger
- RabbitMQ
- SqlServer Database

**will continue to include the rest**
