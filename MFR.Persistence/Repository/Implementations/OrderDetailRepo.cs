using MFR.DomainModels;

namespace MFR.Persistence.Repository.Implementations
{
    public class OrderDetailRepo : BaseRepo<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(MFRDbContext context) : base(context){}

        public MFRDbContext MFRDbContext { get; set; }
    }
}
