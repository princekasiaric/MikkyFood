﻿using MFR.DomainModels;
using MFR.Persistence.ModelValidation;
using Microsoft.EntityFrameworkCore;

namespace MFR.Persistence
{
    public class MFRDbContext : DbContext
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
        }
    }
}
