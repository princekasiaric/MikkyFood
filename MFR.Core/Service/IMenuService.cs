using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IMenuService : IBaseService<MenuRequest, MenuResponse>
    {
        Task DeleteAsync(long Id);
        Task<MenuResponse> CreateAsync(MenuRequest request);
        Task UpdateAsync(long Id, MenuRequest request);
        Task<ICollection<MenuResponse>> GetMenusAsync();
        Task<MenuResponse> GetMenuByIdAsync(long Id);
    }
}
