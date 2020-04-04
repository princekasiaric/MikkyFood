using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using Microsoft.AspNetCore.Hosting;
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

        public async Task<UploadResponse> UploadImageAsync(UploadRequest request)
        {
            UploadResponse uploadPath = null;
            if (request.Image != null && request.Image.Length > 0)
            {
                var uploadDir = @"Resources/Image";
                var contentRootPathWithUploadDir = Path.Combine(_webHostEnvironment.ContentRootPath, uploadDir);
                var fileName = ContentDispositionHeaderValue.Parse(request.Image.ContentDisposition).FileName.Trim('"');
                var path = Path.Combine(contentRootPathWithUploadDir, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }
                uploadPath.ImagePath = "/" + uploadDir + "/" + fileName;
            }
            return uploadPath;
        }
    }
}
