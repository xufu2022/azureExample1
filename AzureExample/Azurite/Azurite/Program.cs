using AzureBlobProject.Services;
using Azurite.Components;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["localstorage:blobServiceUri"]!);
    clientBuilder.AddQueueServiceClient(builder.Configuration["localstorage:queueServiceUri"]!).WithName("localstorage");
    clientBuilder.AddTableServiceClient(builder.Configuration["localstorage:tableServiceUri"]!).WithName("localstorage");
});

builder.Services.AddSingleton<IContainerService, ContainerService>();
builder.Services.AddSingleton<IBlobService, BlobService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Azurite.Client._Imports).Assembly);

app.Run();
