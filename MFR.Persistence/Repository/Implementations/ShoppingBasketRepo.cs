using MFR.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class ShoppingBasketRepo : BaseRepo<ShoppingBasketItem>, IShoppingBasketRepo 
    {
        public string ShoppingBasketId { get; set; }
        public ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }

        public ShoppingBasketRepo(MFRDbContext context) : base(context){ } 

        public async Task<decimal> GetShoppingBasketTotalAsync() 
            => await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                     .Select(sb => sb.SubMenu.Price * sb.Quantity)
                                                     .SumAsync();

        public async Task<ICollection<ShoppingBasketItem>> ClearBasketAsync() 
            => await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                     .ToListAsync();

        public async Task<ShoppingBasketItem> AddToBasketAsync(SubMenu subMenu) 
            => await MFRDbContext.ShoppingBasketItems.FirstOrDefaultAsync(sb => sb.SubMenu.SubMenuId
                                                                             == subMenu.SubMenuId && sb.ShoppingBasketId 
                                                                             == ShoppingBasketId);

        public async Task<ICollection<ShoppingBasketItem>> GetShoppingBasketItemsAsync() 
            => ShoppingBasketItems ?? (ShoppingBasketItems = await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                                                                   .Include(sb => sb.SubMenu)
                                                                                                   .ToListAsync());

        public async Task<ShoppingBasketItem> RemoveFromBasketAsync(SubMenu subMenu)
            => await MFRDbContext.ShoppingBasketItems.FirstOrDefaultAsync(sb => sb.SubMenu.SubMenuId
                                                                             == subMenu.SubMenuId && sb.ShoppingBasketId
                                                                             == ShoppingBasketId);

        public void DeleteBasket(ICollection<ShoppingBasketItem> shoppingBasketItems) 
            => RemoveRange(shoppingBasketItems);

        public void DeleteItemFromBasketAsync(ShoppingBasketItem item) => Remove(item);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
