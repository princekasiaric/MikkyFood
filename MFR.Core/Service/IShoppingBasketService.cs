using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Core.Service
{
    public interface IShoppingBasketService : IBaseService<ShoppingBasketRequest, ShoppingBasketResponse>
    {
        Task ClearBasketAsync();
        string ShoppingBasketId { get; set; }
        Task RemoveFromBasketAsync(SubMenu subMenu);
        Task<ShoppingBasketResponse> GetShoppingBasketItemsAsync();
        Task AddToBasketAsync(ShoppingBasketRequest request, int numberOfItem);
    }
}
