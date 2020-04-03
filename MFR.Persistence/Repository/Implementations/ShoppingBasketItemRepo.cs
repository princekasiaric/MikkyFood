using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class ShoppingBasketItemRepo : BaseRepo<ShoppingBasketItem>, IShoppingBasketItemRepo
    {
        public ShoppingBasketItemRepo(MFRDbContext context) : base(context){ }

        public async Task AddShoppingBasketItemAsync(ShoppingBasketItem shoppingBasketItem) 
            => await Add(shoppingBasketItem);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
