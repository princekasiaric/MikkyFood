using Microsoft.AspNetCore.Http;

namespace MFR.Core.DTO.Request
{
    public class UploadRequest
    {
        public IFormFile Image { get; set; }
    }
}
