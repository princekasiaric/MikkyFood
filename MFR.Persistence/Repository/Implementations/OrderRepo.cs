using MFR.DomainModels;

namespace MFR.Persistence.Repository.Implementations
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(MFRDbContext context) : base(context){}

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
