using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IMenuService : IBaseService<MenuRequest, MenuResponse>
    {
        Task DeleteAsync(long id);
        Task<MenuResponse> CreateAsync(MenuRequest request);
        Task UpdateAsync(long id, MenuRequest request);
        Task<ICollection<MenuResponse>> GetMenusAsync();
        Task<MenuResponse> GetMenuByIdAsync(long id);
    }
}
