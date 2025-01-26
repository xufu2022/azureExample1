using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace AzureTangyFunc
{
    public class ResizeImageOnBlobUpload
    {
        private readonly ILogger<ResizeImageOnBlobUpload> _logger;

        public ResizeImageOnBlobUpload(ILogger<ResizeImageOnBlobUpload> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ResizeImageOnBlobUpload))]
        public async Task Run(
            [Microsoft.Azure.Functions.Worker.BlobTrigger("functionsalesrep/{name}", Connection = "AzureWebJobsStorage")] Stream stream,
            [Blob("functionsalesrep-sm/{name}", FileAccess.Write)] Stream streamOutPut,
            string name)
        {
            try
            {

                using var originalImage = Image.FromStream(stream);
                using var resizedImage = new Bitmap(originalImage, new Size(300, 200));

                // Save the resized image to the output blob
                resizedImage.Save(streamOutPut, ImageFormat.Jpeg);

                //// Ensure the stream is readable and seekable
                //if (!stream.CanRead || !stream.CanSeek)
                //{
                //    _logger.LogError($"Stream for blob {name} is not readable or seekable.");
                //    return;
                //}

                //// Create a MemoryStream to copy the input stream
                //using var memoryStream = new MemoryStream();
                //await stream.CopyToAsync(memoryStream);
                //memoryStream.Position = 0; // Reset position for reading

                //// Load the image from the MemoryStream
                //using var image = await Image.LoadAsync(memoryStream);

                //// Get the image format from the image metadata
                //var format = image.Metadata.DecodedImageFormat;
                //if (format == null)
                //{
                //    _logger.LogError($"Unable to detect image format for blob {name}.");
                //    return;
                //}

                //// Resize the image
                //image.Mutate(x => x.Resize(300, 200));

                //// Save the resized image to the output stream in the original format
                //await image.SaveAsync(streamOutPut, format);

                _logger.LogInformation($"C# Blob trigger function processed blob\n Name: {name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing blob {name}");
                throw; // Re-throw the exception to mark the function as failed
            }
        }
    }
}
