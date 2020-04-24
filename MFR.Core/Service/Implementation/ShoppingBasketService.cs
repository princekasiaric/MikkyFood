using MFR.Core.DTO.Response;
using MFR.DomainModels;
using MFR.Persistence.UnitOfWork;
using System.Threading.Tasks;

namespace MFR.Core.Service.Implementation
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IValueAddedTaxService _valueAddedTax;
        private readonly IUnitOfWork _unitOfWork;
        private decimal total;

        public ShoppingBasketService(IValueAddedTaxService valueAddedTax, 
                                     IUnitOfWork unitOfWork)
        {
            _valueAddedTax = valueAddedTax;
            _unitOfWork = unitOfWork;
        }

        public string ShoppingBasketId { get => _unitOfWork.ShoppingBaskets.ShoppingBasketId; 
                                         set => _unitOfWork.ShoppingBaskets.ShoppingBasketId = value; }

        public async Task AddToBasketAsync(SubMenu subMenu, int numberOfItem)
        {
            var shoppingBasketItem = await _unitOfWork.ShoppingBaskets.AddToBasketAsync(subMenu);
            if (shoppingBasketItem == null)
            {
                shoppingBasketItem = new ShoppingBasketItem
                {
                    SubMenu = subMenu,
                    Quantity = numberOfItem,
                    ShoppingBasketId = ShoppingBasketId
                };
                await _unitOfWork.ShoppingBasketItems.AddShoppingBasketItemAsync(shoppingBasketItem);
            }
            else
            {
                shoppingBasketItem.Quantity++;
            }
            await _unitOfWork.CommitAndSaveChangesAsync();
        }

        public async Task ClearBasketAsync()
        {
            var shoppingBasketItems = await _unitOfWork.ShoppingBaskets.ClearBasketAsync();
            _unitOfWork.ShoppingBaskets.DeleteBasket(shoppingBasketItems);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ShoppingBasketResponse> GetShoppingBasketItemsAsync() 
        {
            ShoppingBasketResponse shoppingBasketResponse = null;
            var shoppingBasketItems = await _unitOfWork.ShoppingBaskets.GetShoppingBasketItemsAsync();
            if (shoppingBasketItems != null)
            {

                foreach (var shoppingBasketItem in shoppingBasketItems)
                {
                    shoppingBasketResponse = new ShoppingBasketResponse
                    {
                        Id = shoppingBasketItem.Id,
                        Name = shoppingBasketItem.SubMenu.Name,
                        Price = shoppingBasketItem.SubMenu.Price,
                        Quantity = shoppingBasketItem.Quantity,
                        CreatedAt = shoppingBasketItem.CreatedAt,
                        SubMenuId = shoppingBasketItem.SubMenuId,
                        Description = shoppingBasketItem.SubMenu.Description,
                        ShoppingBasketTotal = total = await _unitOfWork.ShoppingBaskets.GetShoppingBasketTotalAsync(),
                        VAT = _valueAddedTax.CalculateVat(total)
                    };
                }
            }
            return shoppingBasketResponse;
        }

        public async Task RemoveFromBasketAsync(SubMenu subMenu)
        {
            var shoppingBasketItem = await _unitOfWork.ShoppingBaskets.RemoveFromBasketAsync(subMenu);
            if (shoppingBasketItem != null)
            {
                if (shoppingBasketItem.Quantity > 1)
                {
                    shoppingBasketItem.Quantity--;
                }
                else
                {
                    _unitOfWork.ShoppingBaskets.DeleteItemFromBasketAsync(shoppingBasketItem);
                }
                await _unitOfWork.CommitAndSaveChangesAsync();
            }
        }
    }
}
