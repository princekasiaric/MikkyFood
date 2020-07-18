using System.Threading.Tasks;
using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using MFR.DomainModels;
using MFR.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingBasketsController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        private readonly IShoppingBasketRepo _shoppingBasketRepo;
        private readonly ISubMenuService _subMenuService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public ShoppingBasketsController(IShoppingBasketService shoppingBasketService, 
                                         IShoppingBasketRepo shoppingBasketRepo, 
                                         ISubMenuService subMenuService, 
                                         IOrderService orderService, 
                                         IMapper mapper)
        {
            _shoppingBasketService = shoppingBasketService;
            _shoppingBasketRepo = shoppingBasketRepo;
            _subMenuService = subMenuService;
            _orderService = orderService;
            _mapper = mapper;
            _shoppingBasketService.ShoppingBasketId = _shoppingBasketRepo.ShoppingBasketId;
        }

        [AllowAnonymous]
        [HttpPost("shoppingBasket/{Id}")]
        public async Task<IActionResult> AddToShoppingBasket(long Id) 
        {
            var subMenu = _mapper.Map<SubMenu>(await _subMenuService.GetSubMenuByIdAsync(Id));
            if (subMenu != null)
            {
                await _shoppingBasketService.AddToBasketAsync(subMenu, 1);
                return Ok(new ApiResponse { Status = true, Message = "Success" });
            }
            return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"SubMenu Id '{Id}' not found"
                    });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderRequest request)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(request);
            }
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingBasketItems()
        {
            var response = await _shoppingBasketService.GetShoppingBasketItemsAsync();
            if (response == null)
            {
                return NotFound(new ApiResponse
                {
                    Status = false,
                    Message = "Shopping basket is empty, add item"
                });
            }
            return Ok(new ApiResponse
            {
                Status = true,
                Message = "Success",
                Result = response
            });
        }

        [HttpDelete("Item/{Id}")]
        public async Task<IActionResult> RemoveFromBasket(long Id)
        {
            var subMenu = _mapper.Map<SubMenu>(await _subMenuService.GetSubMenuByIdAsync(Id));
            if (subMenu == null)
            {
                return NotFound(new ApiResponse
                {
                    Status = false,
                    Message = $"Item Id '{Id}' not added to shopping basket"
                });
            }
            await _shoppingBasketService.RemoveFromBasketAsync(subMenu);
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> ClearShoppingBasket()
        {
            await _shoppingBasketService.ClearBasketAsync();
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }
    }
}