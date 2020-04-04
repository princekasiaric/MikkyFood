using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IFileUploadService : IBaseService<UploadRequest, UploadResponse>
    {
        Task<UploadResponse> UploadImageAsync(UploadRequest request);
    }
}
