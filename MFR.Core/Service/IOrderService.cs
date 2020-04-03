using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IOrderService : IBaseService<OrderRequest, OrderResponse>
    {
        Task CreateOrderAsync(OrderRequest request); 
    }
}
