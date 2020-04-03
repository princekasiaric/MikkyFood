using MFR.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class MenuRepo : BaseRepo<Menu>, IMenuRepo
    {
        public MenuRepo(MFRDbContext context) : base(context){ }

        public async Task<ICollection<Menu>> GetAllMenuAsync() 
            => await MFRDbContext.Menus.AsNoTracking().ToListAsync(); 

        public async Task<ICollection<Menu>> GetAllMenuWithSubMenusAsync() 
            => await MFRDbContext.Menus.Include(sb => sb.SubMenus).AsNoTracking().ToListAsync(); 

        public async Task AddMenuAsync(Menu menu) => await Add(menu);

        public void DeleteMenu(Menu menu) => Remove(menu);

        public async Task<Menu> GetMenuOnlyByIdAsync(long id) => await MFRDbContext.Menus.FindAsync(id); 

        public async Task<Menu> GetMenuWithSubMenusByIdAsync(long id) 
            => await MFRDbContext.Menus.Include(sb => sb.SubMenus).FirstOrDefaultAsync(m => m.MenuId == id);

        public void UpdateMenu(Menu menu) => Update(menu);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
