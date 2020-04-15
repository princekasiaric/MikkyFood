using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface ISubMenuService : IBaseService<SubMenuRequest, SubMenuResponse>
    {
        Task DeleteSubMenuAsync(long id);
        Task<SubMenuResponse> GetSubMenuByIdAsync(long id);
        Task<ICollection<SubMenuResponse>> GetAllSubMenuAsync();
        Task UpdateSubMenuAsync(long id, SubMenuRequest request);
        Task<SubMenuResponse> AddSubMenuAsync(SubMenuRequest request);
        Task<ICollection<SubMenuResponse>> GetSubMenuByOrderByNameAsync();
        Task<ICollection<SubMenuResponse>> GetSubMenusByMenuAsync(string menu);
    }
}
