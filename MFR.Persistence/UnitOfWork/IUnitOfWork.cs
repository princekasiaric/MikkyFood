﻿using MFR.Persistence.Repository;
using System;
using System.Threading.Tasks;

namespace MFR.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IMenuRepo Menus { get; }
        IOrderRepo Orders { get; }
        ISubMenuRepo SubMenus { get; }
        IOrderDetailRepo OrderDetails { get; }
        IReservationRepo Reservations { get; } 
        IShoppingBasketRepo ShoppingBaskets { get; }
        IShoppingBasketItemRepo ShoppingBasketItems { get; }

        Task<int> SaveAsync();
        Task<int> CommitAndSaveChangesAsync();
    }
}
