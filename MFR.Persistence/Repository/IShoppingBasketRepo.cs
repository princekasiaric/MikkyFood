using MFR.DomainModels;
using System.Collections.Generic;

namespace MFR.Persistence.Repository
{
    public interface IShoppingBasketRepo //: IBaseRepo<ShoppingBasketItem>
    {
        decimal GetShoppingBasketTotal();
        string ShoppingBasketId { get; set; }
        ICollection<ShoppingBasketItem> ClearBasket();
        ShoppingBasketItem AddToBasket(SubMenu subMenu);
        ICollection<ShoppingBasketItem> GetShoppingBasketItems();
        ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
    }
}
