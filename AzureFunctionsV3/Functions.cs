using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionsV3
{
    public static class Functions
    {

        //TODO - move to a separate class - Model
        public class Profile
        {
            public string id { get; set; }
            public Purchase[] purchases { get; set; }
            public bool isRegistered { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public class Purchase
        {
            public string sku { get; set; }
            public string description { get; set; }
        }


        [FunctionName("GetProfile")]
        public static IActionResult GetProfile(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
          [CosmosDB(
                "profiles-db",
                "Profiles",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "{Query.id}",
                PartitionKey = "{Query.id}"
                )]Profile profileItem,
          ILogger log)
        {
            try
            {
                string reqId = req.Query["id"];
                
                if (profileItem == null)
                {
                    log.LogInformation($"Could not find profile {reqId}");
                    return new NotFoundResult();
                }

                log.LogInformation($"Found the Profile! {reqId}");
                return new OkObjectResult(profileItem);
            }
            catch (Exception ex)
            {
                log.LogError($"Something went wrong. Exception thrown: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //TODO - remove "get" keyword, let's accept only POST for creation
        [FunctionName("AddPurchase")]
        public static IActionResult AddPurchase(
                [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                [CosmosDB("profiles-db","Profiles",
            ConnectionStringSetting = "CosmosDBConnectionString")]out dynamic Profile,
                    ILogger log
            )
        {
            string id = req.Query["id"]; //your computed/generated GUID
            string sku = req.Query["sku"];

            var purchase = new Purchase { sku = sku};
            
            Profile = new { id = id, purchase};

            return (ActionResult)new OkObjectResult("created");
        }

    }
}
