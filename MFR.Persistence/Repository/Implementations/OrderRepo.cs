using MFR.DomainModels;
using System;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(MFRDbContext context) : base(context){ }

        public async Task CreateOrderAsync(Order order, IShoppingBasketRepo shoppingBasket)
        {
            order.OrderPlacedAt = DateTime.UtcNow;
            order.OrderTotalAmount = await shoppingBasket.GetShoppingBasketTotal();
        }

        public async Task AddOrderAsync(Order order) => await Add(order);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
