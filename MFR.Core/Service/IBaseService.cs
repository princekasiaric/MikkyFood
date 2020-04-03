using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;

namespace MFR.Core.Service
{
    public interface IBaseService<TService, UService> where TService : BaseRequest where UService : BaseResponse
    {
    }
}
