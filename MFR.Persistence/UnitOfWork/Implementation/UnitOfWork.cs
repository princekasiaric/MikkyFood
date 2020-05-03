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
        private bool _disposed = false;

        public UnitOfWork(MFRDbContext context)
        {
            _context = context;
        }

        public IMenuRepo Menus => new MenuRepo(_context);

        public IOrderRepo Orders => new OrderRepo(_context);

        public ISubMenuRepo SubMenus => new SubMenuRepo(_context);

        public IOrderDetailRepo OrderDetails => new OrderDetailRepo(_context);

        public IReservationRepo Reservations => new ReservationRepo(_context);

        public IShoppingBasketRepo ShoppingBaskets => new ShoppingBasketRepo(_context);

        public IShoppingBasketItemRepo ShoppingBasketItems => new ShoppingBasketItemRepo(_context);


        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

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
            if (_disposed) return;
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
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
