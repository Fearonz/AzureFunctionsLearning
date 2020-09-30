using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

namespace CalanderServer
{
    public static class Function1
    {
        [FunctionName("Function1")]

        private static async Task CreateItem()
        {
            var cosmosUrl = "https://adrian-cosmos-db.documents.azure.com:443/";
            var cosmoskey = "KHFyiaZ9N9NlxfsqF1mrXGWddce23pUQL9Ms9w1rJKPNVDtzpOpfGrFXyi3r2UwV0cLqaevxuQxHTS9dmdyd8w==";
            var databaseName = "DemoDB";

            CosmosClient client = new CosmosClient(cosmosUrl, cosmoskey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            Container container = await database.CreateContainerIfNotExistsAsync("MyContainerName", "/partitionKeyPath", 400);

            dynamic testItem = new { id = Guid.NewGuid().ToString(), PartitionKey = "MyTestPkValue", details = "its working" };
            var response = await container.CreateItemAsync(testItem);
        }
        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = "Fearon";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "Hello, {name}."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
