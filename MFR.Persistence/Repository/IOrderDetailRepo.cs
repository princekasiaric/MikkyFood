using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IOrderDetailRepo 
    {
        Task AddOrderDetailAsync(OrderDetail orderDetail);
    }
}
