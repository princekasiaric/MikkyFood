using MFR.Persistence.Repository;
using MFR.Persistence.Repository.Implementations;
using System.Threading.Tasks;

namespace MFR.Persistence.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MFRDbContext _context;

        public UnitOfWork(MFRDbContext context)
        {
            _context = context;
            Menus = new MenuRepo(_context);
            Orders = new OrderRepo(_context);
            SubMenus = new SubMenuRepo(_context);
            OrderDetails = new OrderDetailRepo(_context);
            Reservations = new ReservationRepo(_context);
            ShoppingBaskets = new ShoppingBasketRepo(_context);
            ShoppingBasketItems = new ShoppingBasketItem(_context);
        }

        public IMenuRepo Menus { get; }

        public IOrderRepo Orders { get; }

        public ISubMenuRepo SubMenus { get; }

        public IOrderDetailRepo OrderDetails { get; }

        public IReservationRepo Reservations { get; }

        public IShoppingBasketRepo ShoppingBaskets { get; }

        public IShoppingBasketItemRepo ShoppingBasketItems { get; }

        public Task BeginTransactionAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new System.NotImplementedException();
        }

        public int Save()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
