using Azurite.Client.Models;

namespace AzureBlobProject.Services;

public interface IBlobService 
{
    Task<string> GetBlob(string name,string containerName);
    Task<List<string>> GetAllBlobs(string containerName);
    Task<List<Blob>> GetAllBlobsWithUri(string containerName);
    Task UploadBlob(string containerName, string blobName, Stream content);
    Task<bool> DeleteBlob(string name,string containerName);
}
