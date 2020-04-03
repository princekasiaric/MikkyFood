using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface ISubMenuRepo : IBaseRepo<SubMenu>
    {
        void UpdateSubMenu(SubMenu subMenu);
        void DeleteSubMenu(SubMenu subMenu);
        Task AddSubMenuAsync(SubMenu subMenu);
        Task<SubMenu> GetSubMenuByIdAsync(long id);
        Task<ICollection<SubMenu>> GetAllSubMenuAsync();
        Task<ICollection<SubMenu>> GetSubMenuByOrderByNameAsync(); 
    }
}
