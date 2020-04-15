<h1 align="center">
  Demo of a .NET Core 3.1 GraphQL Api with CosmosDb 
</h1>
<hr/>

<div align="center">

<h3 align="center">
  <a href="https://hotchocolate.io/">Hotchocolate Docs</a>
  <span> · </span>
  <a href="https://docs.microsoft.com/en-us/azure/cosmos-db/introduction">CosmosDB Docs</a>
  <span> · </span>
  <a href="https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli">EF Core Docs</a>
  </h3>

This is a small demo of a .NET Core 3.1 GraphQL API using Hotchocolate (Code first) and Cosmos DB with Entity Framework Core as provider. This demo only includes some simple CRUD operations and has no domain model.

</div>

## What’s In This Document

- [Getting started](#getting-started)

## Getting Started

### Prerequisites

* Setup your own <a href="https://docs.microsoft.com/en-us/azure/cosmos-db/create-cosmosdb-resources-portal">Cosmos DB in Azure</a> or install the <a href="https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator">emulator</a>.
* Povide the CosmosDB endpoint and key using the <a href="https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows">secret manager</a> as follows:

```
{
  "CosmosDb:AccountKey": "your account key here",
  "CosmosDb:AccountEndpoint": "your account endpoint here"
}
```

### Running

* The GraphQL Playground runs at  <a href="https://localhost:44370/playground/">https://localhost:44370/playground/</a>
* Tests do not require Cosmos Db since they use the inmemory database provided by EF Core
