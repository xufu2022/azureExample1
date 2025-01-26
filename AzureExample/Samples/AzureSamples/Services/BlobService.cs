using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureSamples.Models;

namespace AzureSamples.Services
{
    public class BlobService: IBlobService
    {
        private readonly BlobServiceClient _blobClient;
        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task UploadBlob(UploadModel model)
        {
            var containerName = "mycontainer";
            
            var blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

            foreach (var file in model.Files)
            {
                using var stream= file.OpenReadStream();

                var blobClient = blobContainerClient.GetBlobClient($"Vehicle{model.VehicleId}/{file.Name}");
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            //var httpHeaders = new BlobHttpHeaders()
            //{
            //    ContentType = file.ContentType
            //};
        }
    }
}
