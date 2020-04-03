using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IShoppingBasketRepo 
    {
        string ShoppingBasketId { get; set; }
        Task<decimal> GetShoppingBasketTotal();
        void DeleteItemFromBasket(ShoppingBasketItem item);
        Task<ICollection<ShoppingBasketItem>> ClearBasket();
        Task<ShoppingBasketItem> AddToBasket(SubMenu subMenu);
        Task<ShoppingBasketItem> RemoveFromBasket(SubMenu subMenu);
        Task<ICollection<ShoppingBasketItem>> GetShoppingBasketItems();
        ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
        void DeleteBasket(ICollection<ShoppingBasketItem> shoppingBasketItems);
    }
}
