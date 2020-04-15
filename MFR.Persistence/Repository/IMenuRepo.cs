using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IMenuRepo 
    {
        void DeleteMenu(Menu menu);
        Task AddMenuAsync(Menu menu);
        void UpdateMenu(Menu menu);
        Task<Menu> GetMenuOnlyByIdAsync(long id);
        Task<ICollection<Menu>> GetAllMenuAsync();
    }
}
