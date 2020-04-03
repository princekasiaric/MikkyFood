using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class OrderDetailRepo : BaseRepo<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(MFRDbContext context) : base(context){}

        public async Task AddOrderDetailAsync(OrderDetail orderDetail) 
            => await Add(orderDetail);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
