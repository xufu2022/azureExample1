using System.Xml.Linq;
using Azure.Storage.Queues;
using AzureTangyFunc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureTangyFunc
{
    public class OnSalesUploadWriteToQueue
    {
        private readonly ILogger<OnSalesUploadWriteToQueue> _logger;
        private readonly QueueServiceClient _queueServiceClient;
        public OnSalesUploadWriteToQueue(QueueServiceClient queueServiceClient,ILogger<OnSalesUploadWriteToQueue> logger)
        {
            _queueServiceClient = queueServiceClient;
            _logger = logger;
        }

        [Function("OnSalesUploadWriteToQueue")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            //[Queue("salesrequestinbound", Connection = "AzureWebJobsStorage")] IAsyncCollector<SalesRequest> salesRequestQueue,
            ILogger log)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody =await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<SalesRequest>(requestBody);

            //await salesRequestQueue.AddAsync(data);
           // var queueClient = _queueServiceClient.GetQueueClient("salesrequestinbound-poison");
            //string message = JsonSerializer.Serialize(requestBody);
          //  await queueClient.SendMessageAsync(requestBody);

            var stringRespone= $"Sales Request added to the queue {data.Name}";
            return new OkObjectResult(stringRespone);
        }
    }
}
