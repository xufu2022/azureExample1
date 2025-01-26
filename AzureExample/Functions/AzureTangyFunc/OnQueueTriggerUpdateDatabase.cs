using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureTangyFunc
{
    public class OnQueueTriggerUpdateDatabase
    {
        private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;

        public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger)
        {
            _logger = logger;
        }

        //not working and queue data get lost
        [Function(nameof(OnQueueTriggerUpdateDatabase))]
        public void Run([QueueTrigger("salesrequestinbound-poison", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {

            try
            {
                _logger.LogInformation($"C# Queue trigger function processed: {message}");
                // Add your database update logic here
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing queue message: {ex.Message}");
                throw;
            }
        }
    }
}
