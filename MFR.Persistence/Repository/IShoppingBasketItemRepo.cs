using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IShoppingBasketItemRepo 
    {
        Task AddShoppingBasketItemAsync(ShoppingBasketItem shoppingBasketItem);
    }
}
