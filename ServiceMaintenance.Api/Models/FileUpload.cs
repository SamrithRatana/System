using Microsoft.AspNetCore.Http;

namespace ServiceMaintenance.Api.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }

    }
}
