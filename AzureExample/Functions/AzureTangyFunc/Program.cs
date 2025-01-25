using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddAzureClients(clientBuilder =>
{

        // Adding a QueueServiceClient using a connection string from configuration
        clientBuilder.AddQueueServiceClient(builder.Configuration.GetSection("AzureWebJobsStorage"));


    // Optionally, you can use managed identity if running in Azure, example:
    // builder.AddBlobServiceClient(new Uri("<your-blob-service-uri>"))
    //        .WithCredential(new DefaultAzureCredential());
});

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
