<img src="https://ci.appveyor.com/api/projects/status/eh969xbccti5ow78/branch/master?svg=true" alt="Project Badge" width="300">

## Getting started

The project aims to quickly configure the CQRS architecture.
There are two settings ways:
1. Api -> inprocess command & query handlers
2. Api -> NATS message bus -> hosts which handle messages and send them with inprocess handlers

Ioc config helper you can find [here](docs/ProjectBuilder.cs)
All projects has nuget packages, all of them starts with: In.Infrastructure

## Service configuration

Configurations:
1. In.Common:
  _ There are services and helpers _
 Ioc method: **AddCommonServices**
 
 Adds:
 - IDiScope:      service locator
 - ITypeFactory:  try to get Type be string
 Helpers:
 - AsyncHelpers:  runs task in the sync mode
 - IocExtensions: extensions for Ioc:
      `RegisterAssemblyImplementationsScoped,
      RegisterAssemblyImplementationsSingleton,
      AddScopedGenerics`
      
2. In.Web:
  There are services and middleware
  Ioc method: **AddWebServices**
  Adds:
  - IUserContextService:  uses in controller for getting user data
  
  Middleware method: **UseErrorsMiddleware**
  - Unhandled exceptions wrapper
3. In.Auth:
  There are configuration extensions:
    - AddAuthOptions
    - AddAuth

3. In.Specifications
  There are Specification pattern and a helper:
  **Specifications** - Main functional class, have and/or/not implementations
  **Utility** - helpers

4. In.Logging
  There are services for logging
  Ioc method: **AddLoggerServices**
  Add:
    - ILogService: logging methods
    
5. In.FunctionalCSharp
  There are Functional extensions for business logic
  **Result** - Main functional class
  
6. In.DDD (**Not ready yet!**)
  There are domain events and handlers
  Ioc method: **AddWebServices**

7. In.DataMapping
  There are IMapperService abstraction only
  **Implementations libs**:
    - In.DataMapping.Automapper
  
8. In.DataAccess
  There are abstractions for Domain like IHasKey
  Repositury abstractions:
    - IDatasetUow
    - ILinqProvider
    - IRepository
    
  **Implementations libs**:
    - In.DataAccess.EfCore
    - In.DataAccess.Mongo
    
9. In.Cqrs.Command
  There are abstractions for commands and handlers
  Abstractions:
    - ICommandHandler
    - IMessage
    - IMessageResult
    - IMessageSender
  
  **Implementations libs**:
    - In.Cqrs.Command.Simple
        There are simple in proccess message bus
        Ioc ext: **AddCommandServices** - registres:
          - IMessageResult
          - SimpleMsgBus
          - Lookup for handlers in assemblies in param
    - In.Cqrs.Command.Nats
        There are implementations for NatsMsgBus and queue & reply implementations
        
10. In.Cqrs.Query
    There are abstractions for commands and handlers
    Abstractions:
      - ICriterion
      - IQueryBuilder
      - IQueryFactory
      - IQueryFor
      - IQueryHandler
      - IGenericQuery
      - IGenericQueryBuilder
      - ISingleQueryResult
      - IMultipleQueryResult
      
     **Implementations libs**:
     - In.Cqrs.Query.Simple
      There are simple im process message handlers
     - In.Cqrs.Query.Nats
      There are Nats query handlers implementations for a master and a slave
      