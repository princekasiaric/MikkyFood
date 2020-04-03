using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IFileUploadService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
