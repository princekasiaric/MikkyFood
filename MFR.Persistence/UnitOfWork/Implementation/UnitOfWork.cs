using MFR.Persistence.Repository;
using MFR.Persistence.Repository.Implementations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace MFR.Persistence.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MFRDbContext _context;

        private bool _disposed;

        public UnitOfWork(MFRDbContext context)
        {
            _context = context;
            Menus = new MenuRepo(_context);
            Orders = new OrderRepo(_context);
            SubMenus = new SubMenuRepo(_context);
            OrderDetails = new OrderDetailRepo(_context);
            Reservations = new ReservationRepo(_context);
            ShoppingBaskets = new ShoppingBasketRepo(_context);
            ShoppingBasketItems = new ShoppingBasketItemRepo(_context);
        }

        public IMenuRepo Menus { get; }

        public IOrderRepo Orders { get; }

        public ISubMenuRepo SubMenus { get; }

        public IOrderDetailRepo OrderDetails { get; }

        public IReservationRepo Reservations { get; }

        public IShoppingBasketRepo ShoppingBaskets { get; } 

        public IShoppingBasketItemRepo ShoppingBasketItems { get; }


        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public int Save() => _context.SaveChanges();

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CommitAndSaveChangesAsync()
        {
            int affectedRow = 0;
            using (IDbContextTransaction Transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    affectedRow = await _context.SaveChangesAsync();
                    await Transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await Transaction.RollbackAsync();
                }
                return affectedRow;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
