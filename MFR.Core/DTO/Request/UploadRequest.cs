using Microsoft.AspNetCore.Http;

namespace MFR.Core.DTO.Request
{
    public class UploadRequest : BaseRequest
    {
        public IFormFile Image { get; set; }
    }
}
