# Todo Project 

## Get Started

```bash
docker-compose up -d postgresql
docker-compose up -d rabbitmq
docker-compose up -d --build api
docker-compose up -d --build ui.blazor
```


### Swagger API

<http://localhost:6001/swagger/index.html>


### RabbitMQ

<http://localhost:15672>


### Blazor UI

<http://localhost:6002>


<br>


## Migrations

```bash
dotnet ef migrations add <migrationName> -s src/Todo.Api -p src/Todo.Infrastructure
dotnet ef database update <migrationName> -s src/Todo.Api -p src/Todo.Infrastructure
dotnet ef migrations remove -s src/Todo.Api -p src/Todo.Infrastructure
```