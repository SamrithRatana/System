using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceMaintenance.Controllers
{
    public class AudioUploadController : Controller
    {
        [Route("uploadAudio")]
        [HttpPost]
        public async Task<IActionResult> UploadAudio()
        {
            var file = Request.Form.Files.GetFile("audio");

            if (file != null && file.Length > 0)
            {
                // Generate a unique filename using a GUID and the original file extension
                var fileExtension = Path.GetExtension(file.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok($"/uploads/{uniqueFileName}"); // Return the URL for the uploaded file
            }

            return BadRequest("No file uploaded");
        }
    }
}
