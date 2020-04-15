using MFR.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IShoppingBasketRepo 
    {
        string ShoppingBasketId { get; set; }
        Task<decimal> GetShoppingBasketTotalAsync();
        void DeleteItemFromBasketAsync(ShoppingBasketItem item);
        Task<ICollection<ShoppingBasketItem>> ClearBasketAsync();
        Task<ShoppingBasketItem> AddToBasketAsync(SubMenu subMenu);
        Task<ShoppingBasketItem> RemoveFromBasketAsync(SubMenu subMenu);
        Task<ICollection<ShoppingBasketItem>> GetShoppingBasketItemsAsync();
        ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
        void DeleteBasket(ICollection<ShoppingBasketItem> shoppingBasketItems);
    }
}
