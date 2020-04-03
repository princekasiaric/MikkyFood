using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Utils;
using MFR.DomainModels;
using MFR.Persistence.UnitOfWork;
using System.Threading.Tasks;

namespace MFR.Core.Service.Implementation
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IValueAddedTaxService _valueAddedTax;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private decimal total;

        public ShoppingBasketService(IValueAddedTaxService valueAddedTax, 
                                     IUnitOfWork unitOfWork, 
                                     IMapper mapper)
        {
            _valueAddedTax = valueAddedTax;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string ShoppingBasketId { get => _unitOfWork.ShoppingBaskets.ShoppingBasketId; 
                                         set => _unitOfWork.ShoppingBaskets.ShoppingBasketId = value; }

        public async Task AddToBasket(ShoppingBasketRequest request, int numberOfItem)
        {
            var subMenu = _mapper.Map<SubMenu>(request);
            var shoppingBasketItem = await _unitOfWork.ShoppingBaskets.AddToBasket(subMenu);
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
            var shoppingBasketItems = await _unitOfWork.ShoppingBaskets.ClearBasket();
            _unitOfWork.ShoppingBaskets.DeleteBasket(shoppingBasketItems);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ShoppingBasketResponse> GetShoppingBasketItems() 
        {
            ShoppingBasketResponse shoppingBasketResponse = null;
            var shoppingBasketItems = await _unitOfWork.ShoppingBaskets.GetShoppingBasketItems();
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
                        ShoppingBasketTotal = total = await _unitOfWork.ShoppingBaskets.GetShoppingBasketTotal(),
                        VAT = _valueAddedTax.CalculateVat(total)
                    };
                }
            }
            else
            {
                throw new EmptyShoppingBasketException($"Add item to shoppingBasket.");
            }
            return shoppingBasketResponse;
        }

        public async Task RemoveFromBasketAsync(SubMenu subMenu)
        {
            //var subMenu = await _unitOfWork.SubMenus.GetSubMenuByIdAsync(id); // This goes to controller
            var shoppingBasketItem = await _unitOfWork.ShoppingBaskets.RemoveFromBasket(subMenu);
            if (shoppingBasketItem != null)
            {
                if (shoppingBasketItem.Quantity > 1)
                {
                    shoppingBasketItem.Quantity--;
                }
                else
                {
                    _unitOfWork.ShoppingBaskets.DeleteItemFromBasket(shoppingBasketItem);
                }
                await _unitOfWork.CommitAndSaveChangesAsync();
            }
        }
    }
}
