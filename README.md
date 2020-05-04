# Jobsity StockChat

Jobsity challenge app for a stock community chat.

# Features
 
## Included
- Web Api
- Real time message bus
- Api Client
- Unit test

## Missing
- Web App
- Chat bot
- Installer

# Stack
- .Net Core 3.1
- Asp .Net Core 3.1
- Asp .Net Core SignalR 3.1
- EntityFrameworkCore
- CosmosDb
- MediaTr
- NUnit
- NSubstitute

# Requirements
- .Net Core SDK 3.1 or greater
- Asp .Net Core SDK 3.1 or greater
- Azure CosmosDb subscriptions or Azure CosmosDb Emulator for Desktop
  
# Setup

## Installation.
- Install [.Net Core 3.1S SD](https://dotnet.microsoft.com/)
- Install [Azure CosmosDb Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)

## Run CosmosDb Emulato
- Open your command prompt on the following folder `C:\Program Files\Azure Cosmos DB Emulator`
- Run the following command `CosmosDb.Emulator.exe /NoFirewall /Port=8081 /Key:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==`

## Run the project
- Open your command prompt on the repo code base folder `{RepositoryLocalPath}\src`
- Run the command `dotnet restore`
- Start the project by running `dotnet run`
  
## Test the project
- Open your command prompt on the repo code base folder `{RepositoryLocalPath}\src`
- Run the command `dotnet restore`
- Run all test in the project by running `dotnet test`
