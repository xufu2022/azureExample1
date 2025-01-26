using AzureSamples.Models;

namespace AzureSamples.Services
{
    public interface IBlobService
    {
        Task UploadBlob(UploadModel model);
    }
}