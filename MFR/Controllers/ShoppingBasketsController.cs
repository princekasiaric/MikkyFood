using System.Threading.Tasks;
using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.Core.Service;
using MFR.DomainModels;
using MFR.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MFR.Controllers
{
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

        [HttpGet("basket/{id}")]
        public async Task<IActionResult> AddToShoppingBasketAsync(long id)
        {
            var subMenu = _mapper.Map<SubMenu>(await _subMenuService.GetSubMenuByIdAsync(id));
            if (subMenu != null)
            {
                await _shoppingBasketService.AddToBasketAsync(subMenu, 1);
            }
            return NotFound(
                    new ApiResponse
                    {
                        Status = false,
                        Message = $"SubMenu Id '{id}' not found"
                    });
        }

        [HttpPost("order")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrderAsync([FromBody]OrderRequest request)
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

        [HttpGet("items")]
        public async Task<IActionResult> GetShoppingBasketItemsAsync()
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

        [HttpDelete("item/{id}")]
        public async Task<IActionResult> RemoveFromBasketAsync(long id)
        {
            var subMenu = _mapper.Map<SubMenu>(await _subMenuService.GetSubMenuByIdAsync(id));
            if (subMenu == null)
            {
                return NotFound(new ApiResponse
                {
                    Status = false,
                    Message = $"Item Id '{id}' not added to shopping basket"
                });
            }
            await _shoppingBasketService.RemoveFromBasketAsync(subMenu);
            return StatusCode(204, new ApiResponse
            {
                Status = true,
                Message = "Success"
            });
        }

        [HttpDelete("basket")]
        public async Task<IActionResult> ClearShoppingBasketAsync()
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