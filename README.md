# AzureDemos
HOWTO create a CosmosDB on Azure and also Azure Functions using HttpTriggers to read to/from CosmosDB

Update app.config from CosmosGettingStarted - get Keys from Azure Portal
 - EndpointUri

 - PrimaryKey

run CosmosGettingStarted (set as startup project). This will create a new CosmosDB with 1 record


Update local.settings.json from AzureFunctionsV3 - get Keys from Azure Portal

 - "CosmosDBConnectionString" == PRIMARY CONNECTION STRING

run AzureFunctionsV3 (set as startup project). This will create two local endpoints

Local Test using any browser
http://localhost:7071/api/GetProfile?id=41bb417e-c942-4df2-96f0-cd2c3e1b2f91

http://localhost:7071/api/AddPurchase?id=1&sku=1

local.settings.json sample
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "CosmosDBConnectionString": ""
  }
}