using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IOrderRepo 
    {
        Task AddOrderAsync(Order order);
        Task CreateOrderAsync(Order order, IShoppingBasketRepo shoppingBasket);
    }
}
