using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AzureSamples.Models
{

    public class UploadModel
    {
        [Required(ErrorMessage = "Vehicle ID is required.")]
        public string VehicleId { get; set; }

        public IReadOnlyList<IBrowserFile> Files { get; set; }
    }
}
