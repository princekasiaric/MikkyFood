using MFR.Models;
using Microsoft.EntityFrameworkCore;

namespace MFR.Persistence.ModelValidation
{
    public static class DomainModelConfiguration
    {
        public static void ConfigureMenuConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(m => m.RowVersion).IsRowVersion();
                entity.Property(m => m.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureSubMenuConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubMenu>(entity =>
            {
                entity.Property(sm => sm.RowVersion).IsRowVersion();
                entity.Property(sm => sm.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureOrderConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.RowVersion).IsRowVersion();
                entity.Property(o => o.FirstName).HasMaxLength(25).IsRequired();
                entity.Property(o => o.LastName).HasMaxLength(40).IsRequired();
                entity.Property(o => o.Address).HasMaxLength(100).IsRequired();
                entity.Property(o => o.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureOrderDetailConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(od => od.RowVersion).IsRowVersion();
                entity.Property(od => od.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureReservationConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(r => r.RowVersion).IsRowVersion();
                entity.Property(r => r.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureShoppingBasketItemConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingBasketItem>(entity =>
            {
                entity.Property(sb => sb.RowVersion).IsRowVersion();
                entity.Property(sb => sb.CreatedAt).ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}
