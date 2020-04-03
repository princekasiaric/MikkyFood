﻿using MFR.DomainModels;
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

        public static ShoppingBasketRepo GetBasket(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<MFRDbContext>();
            var basketId = session.GetString("basketId") ?? Guid.NewGuid().ToString();
            session.SetString("basketId", basketId);

            return new ShoppingBasketRepo(context) { ShoppingBasketId = basketId };
        }

        public async Task<decimal> GetShoppingBasketTotal() 
            => await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                     .Select(sb => sb.SubMenu.Price * sb.Quantity)
                                                     .SumAsync();

        public async Task<ICollection<ShoppingBasketItem>> ClearBasket() 
            => await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                     .ToListAsync();

        public async Task<ShoppingBasketItem> AddToBasket(SubMenu subMenu) 
            => await MFRDbContext.ShoppingBasketItems.FirstOrDefaultAsync(sb => sb.SubMenu.SubMenuId
                                                                             == subMenu.SubMenuId && sb.ShoppingBasketId 
                                                                             == ShoppingBasketId);

        public async Task<ICollection<ShoppingBasketItem>> GetShoppingBasketItems() 
            => ShoppingBasketItems ?? (ShoppingBasketItems = await MFRDbContext.ShoppingBasketItems.Where(sb => sb.ShoppingBasketId == ShoppingBasketId)
                                                                                                   .Include(sb => sb.SubMenu)
                                                                                                   .ToListAsync());

        public async Task<ShoppingBasketItem> RemoveFromBasket(SubMenu subMenu)
            => await MFRDbContext.ShoppingBasketItems.FirstOrDefaultAsync(sb => sb.SubMenu.SubMenuId
                                                                             == subMenu.SubMenuId && sb.ShoppingBasketId
                                                                             == ShoppingBasketId);

        public void DeleteBasket(ICollection<ShoppingBasketItem> shoppingBasketItems) 
            => RemoveRange(shoppingBasketItems);

        public void DeleteItemFromBasket(ShoppingBasketItem item) => Remove(item);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}