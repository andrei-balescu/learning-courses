# .NET Microservices – Full Course for Beginners
## by Julio Casal

Learn the foundational elements of a microservices architecture with .NET in this beginner level course. You will incrementally building a real microservices-based application with the .NET platform and C#.

## Resources
- Video tutorial: https://www.youtube.com/watch?v=CqCDOosvZIk
- Authentication / authorization functionality: https://www.youtube.com/watch?v=Nw4AZs1kLAs

## Project
**Description**: The project consists of a play store for a video game. The store's catalog contains in-game items (potions, weapons, armors) which can be purchased by players.
- Game masters can add, update and remove items from the catalog.
- Players can browse the catalog and purchase items, which will then be added to their inventory.

**Tech stack**  
- Auth microservice: .NET 6 WebAPI - provides user registration / authentication
    - using .NET Core identity
    - using SQL Server DB
- Catalog microservice: .NET 6 WebAPI - performs CRUD operations on items in the DB
    - using MongoDB storage
    - posts updates to message broker (RabbitMQ)
    - authorization using JWT tokens
- Inventory microservice: .NET 6 WebAPI - grants items from the catalog to the player's inventory
    - MongoDB storage
    - synchronizes local DB with any updates comming from message broker (RabbitMQ)
    - authorization using JWT tokens
- Client: .NET 6 MVC - Façade providing UI for microservice functionality
    - Client request resilience using Polly
    - authorization using JWT tokens
- Unit testing using MSTest and Moq
- Docker for containerization

![Microservice project](microservice_architecture.svg)