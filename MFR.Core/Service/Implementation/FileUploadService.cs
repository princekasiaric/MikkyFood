using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MFR.Core.Service.Implementation
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var imageUrl = string.Empty;
            if (file != null && file.Length > 0)
            {
                var uploadDir = @"Resources/Image";
                var contentRootPathWithUploadDir = Path.Combine(_webHostEnvironment.ContentRootPath, uploadDir);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var path = Path.Combine(contentRootPathWithUploadDir, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                imageUrl = "/" + uploadDir + "/" + fileName;
            }
            return imageUrl;
        }
    }
}
