using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IShoppingBasketRepo 
    {
        Task<decimal> GetShoppingBasketTotal();
        string ShoppingBasketId { get; set; }
        Task<ICollection<ShoppingBasketItem>> ClearBasket();
        Task<ShoppingBasketItem> AddToBasket(SubMenu subMenu);
        Task<ICollection<ShoppingBasketItem>> GetShoppingBasketItems();
        ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
    }
}
