using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
//using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Google.Cloud.Translation;
using System.Security.Cryptography.X509Certificates;
using Google.Cloud.Translation.V2;

namespace CalanderServer
{
    class Insertitems
    {
        [FunctionName("InsertItem")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection")]
            out ToDoItem document,
            ILogger log)
        {

            var client = TranslationClient.Create();
            var text = "Hello";
            var response = client.TranslateText(text, LanguageCodes.Korean, LanguageCodes.English);


            try
            {
                var content = response.TranslatedText;
                string jsonContent = content;
                document = JsonConvert.DeserializeObject<ToDoItem>(jsonContent);

                //log.LogInformation($"C# Queue trigger function inserted one row");

                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }

      

    }
}
