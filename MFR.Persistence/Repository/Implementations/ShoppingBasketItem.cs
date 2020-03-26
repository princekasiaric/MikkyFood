namespace MFR.Persistence.Repository.Implementations
{
    public class ShoppingBasketItem : BaseRepo<ShoppingBasketItem>
    {
        public ShoppingBasketItem(MFRDbContext context) : base(context){}

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
