using MFR.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class SubMenuRepo : BaseRepo<SubMenu>, ISubMenuRepo
    {
        public SubMenuRepo(MFRDbContext context) : base(context){}

        public async Task AddSubMenuAsync(SubMenu subMenu) => await Add(subMenu);

        public void DeleteSubMenu(SubMenu subMenu) => Remove(subMenu);

        public async Task<ICollection<SubMenu>> GetAllSubMenuAsync() 
            => await MFRDbContext.SubMenus.ToListAsync();

        public async Task<SubMenu> GetSubMenuByIdAsync(long id) 
            => await MFRDbContext.SubMenus.FindAsync(id);

        public async Task<ICollection<SubMenu>> GetSubMenuByOrderByNameAsync() 
            => await MFRDbContext.SubMenus.OrderBy(sb => sb.Name).ToListAsync();

        public void UpdateSubMenu(SubMenu subMenu) => MFRDbContext.Update(subMenu);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
