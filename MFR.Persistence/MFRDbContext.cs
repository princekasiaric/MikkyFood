using MFR.DomainModels;
using MFR.DomainModels.Identity;
using MFR.Persistence.ModelValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MFR.Persistence
{
    public class MFRDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ShoppingBasketItem> ShoppingBasketItems { get; set; }

        public MFRDbContext(DbContextOptions<MFRDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureMenuConstraints();
            modelBuilder.ConfigureSubMenuConstraints();
            modelBuilder.ConfigureOrderConstraints();
            modelBuilder.ConfigureOrderDetailConstraints();
            modelBuilder.ConfigureReservationConstraints();
            modelBuilder.ConfigureShoppingBasketItemConstraints();

            modelBuilder.SeededMenu();
            modelBuilder.SeededSubMenu();
            modelBuilder.SeededRole();
        }
    }
}
