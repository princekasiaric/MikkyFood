using MFR.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace MFR.Persistence.ModelValidation
{
    public static class DomainModelConfiguration
    {
        public static void ConfigureMenuConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(m => m.MenuId);
                entity.Property(m => m.Name).IsRequired();
                entity.Property(m => m.Image).IsRequired();
                entity.HasMany(m => m.SubMenus).WithOne(sm => sm.Menu);
                entity.Property(m => m.RowVersion).IsRowVersion().IsRequired();
                entity.Property(m => m.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureSubMenuConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubMenu>(entity =>
            {
                entity.HasKey(sm => sm.SubMenuId);
                entity.Property(sm => sm.Name).IsRequired();
                entity.Property(sm => sm.Description).IsRequired(false);
                entity.Property(sm => sm.Image).IsRequired().IsRequired(false);
                entity.Property(sm => sm.RowVersion).IsRowVersion().IsRequired();
                entity.Property(sm => sm.Price).HasColumnType("money").IsRequired();
                entity.HasOne(sm => sm.Menu).WithMany(m => m.SubMenus).HasForeignKey(sm => sm.MenuId);
                entity.Property(sm => sm.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureOrderConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.Email).IsRequired();
                entity.Property(o => o.OrderMethod).IsRequired();
                entity.Property(o => o.PhoneNumber).IsRequired();
                entity.Property(o => o.PaymentMethod).IsRequired();
                entity.HasMany(o => o.OrderDetails).WithOne(od => od.Order);
                entity.Property(o => o.RowVersion).IsRowVersion().IsRequired();
                entity.Property(o => o.Location).IsRequired().HasMaxLength(15);
                entity.Property(o => o.LastName).HasMaxLength(40).IsRequired();
                entity.Property(o => o.Address).HasMaxLength(100).IsRequired();
                entity.Property(o => o.FirstName).HasMaxLength(25).IsRequired();
                entity.Property(o => o.OrderTotalAmount).HasColumnType("money").IsRequired();
                entity.Property(o => o.OrderPlacedAt).HasColumnName("Date Ordered").IsRequired();
                entity.Property(o => o.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureOrderDetailConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => od.Id);
                entity.Property(od => od.Quantity).IsRequired();
                entity.Property(od => od.RowVersion).IsRowVersion().IsRequired();
                entity.Property(od => od.Price).HasColumnType("money").IsRequired();
                entity.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);
                entity.Property(od => od.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureReservationConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Time).IsRequired();
                entity.Property(r => r.NumberOfPeople).IsRequired();
                entity.Property(r => r.RowVersion).IsRowVersion().IsRequired();
                entity.Property(r => r.Date).HasColumnName("Date Booked").IsRequired();
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }

        public static void ConfigureShoppingBasketItemConstraints(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingBasketItem>(entity =>
            {
                entity.HasKey(sb => sb.Id);
                entity.Property(sb => sb.Quantity).IsRequired();
                entity.Property(sb => sb.ShoppingBasketId).IsRequired();
                entity.Property(sb => sb.RowVersion).IsRowVersion().IsRequired();
                entity.Property(sb => sb.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}
