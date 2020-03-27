﻿// <auto-generated />
using System;
using MFR.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MFR.Persistence.Migrations
{
    [DbContext(typeof(MFRDbContext))]
    partial class MFRDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MFR.DomainModels.Menu", b =>
                {
                    b.Property<long>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            MenuId = 1L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 780, DateTimeKind.Local).AddTicks(2781),
                            Name = "African Starter"
                        },
                        new
                        {
                            MenuId = 2L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1065),
                            Name = "Protein"
                        },
                        new
                        {
                            MenuId = 3L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1178),
                            Name = "Swallow"
                        },
                        new
                        {
                            MenuId = 4L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1189),
                            Name = "Soup"
                        },
                        new
                        {
                            MenuId = 5L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1195),
                            Name = "Sides & Salad"
                        },
                        new
                        {
                            MenuId = 6L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1202),
                            Name = "Porridge"
                        },
                        new
                        {
                            MenuId = 7L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1208),
                            Name = "Rice Dish"
                        },
                        new
                        {
                            MenuId = 8L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1214),
                            Name = "Desert"
                        },
                        new
                        {
                            MenuId = 9L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1221),
                            Name = "Chef's Special"
                        },
                        new
                        {
                            MenuId = 10L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 786, DateTimeKind.Local).AddTicks(1227),
                            Name = "Beverages"
                        });
                });

            modelBuilder.Entity("MFR.DomainModels.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int>("OrderMethod")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderPlacedAt")
                        .HasColumnName("Date Ordered")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotalAmount")
                        .HasColumnType("money");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MFR.DomainModels.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long>("SubMenuId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SubMenuId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("MFR.DomainModels.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<DateTime>("Date")
                        .HasColumnName("Date Booked")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("MFR.DomainModels.ShoppingBasketItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("ShoppingBasketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SubMenuId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SubMenuId");

                    b.ToTable("ShoppingBasketItems");
                });

            modelBuilder.Entity("MFR.DomainModels.SubMenu", b =>
                {
                    b.Property<long>("SubMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("SubMenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("SubMenus");

                    b.HasData(
                        new
                        {
                            SubMenuId = 1L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(4391),
                            Description = "Seafood okro is a delightful and delicious mix of all your favourite seafood and okro.",
                            MenuId = 9L,
                            Name = "Seafood Okoro",
                            Price = 2500m
                        },
                        new
                        {
                            SubMenuId = 2L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(5994),
                            Description = "Ofe Nsala (Nsala Soup) is a soup popular in the eastern part of Nigeria. It is also known as ''white soup'' nsala soup is know for its light texture.",
                            MenuId = 9L,
                            Name = "Ofe Nsala",
                            Price = 1500m
                        },
                        new
                        {
                            SubMenuId = 3L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6058),
                            Description = "Abacha and Ugba also known as African salad is a Cassava based dish from the Igbo tribe of Eastern Nigeria.",
                            MenuId = 9L,
                            Name = "Abacha & Ugba",
                            Price = 1500m
                        },
                        new
                        {
                            SubMenuId = 4L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6068),
                            Description = "This bitter leaf soup recipe (also know as Ofe Onugbu) is generously stocked with flavoursome meats, fish and cocoyams. Make it when you’re in the mood for something warm, serve with your favourite swallow, tuck in and enjoy.",
                            MenuId = 9L,
                            Name = "Bitterleaf",
                            Price = 1000m
                        },
                        new
                        {
                            SubMenuId = 5L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6074),
                            Description = " A tasty and hearty Nigerian soup made from Ogbono seeds (bush mango seeds) added with pre-cooked meat.",
                            MenuId = 4L,
                            Name = "Ogbono",
                            Price = 500m
                        },
                        new
                        {
                            SubMenuId = 6L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6081),
                            Description = "The African Stewed Spinach also popularly known as Efo riro is a one-pot stew with layers of flavor.",
                            MenuId = 4L,
                            Name = "Efo riro",
                            Price = 500m
                        },
                        new
                        {
                            SubMenuId = 7L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6088),
                            Description = "Pounded Yam is one of the best Nigeria swallows that is eaten with the various delicious Nigerian soups. it is made with white boiled yam.",
                            MenuId = 3L,
                            Name = "Pounded Yam",
                            Price = 250m
                        },
                        new
                        {
                            SubMenuId = 8L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6095),
                            Description = "Àmàlà is a local indigenous Nigerian food, native to the Yoruba tribe in the South-western parts of the country.",
                            MenuId = 3L,
                            Name = "Amala",
                            Price = 150m
                        },
                        new
                        {
                            SubMenuId = 9L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6102),
                            Description = "Peppered Gizzard is simply Nigerian stewed gizzards.",
                            MenuId = 2L,
                            Name = "Pepper Gizzard",
                            Price = 500m
                        },
                        new
                        {
                            SubMenuId = 10L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6109),
                            Description = "Goat meat or goat's meat is the meat of the domestic goat.",
                            MenuId = 2L,
                            Name = "Goat Meat",
                            Price = 250m
                        },
                        new
                        {
                            SubMenuId = 11L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6116),
                            Description = "Goat meat pepper soup, also referred to as nwo-nwo, ngwo-ngwo, and goat pepper soup, is a soup in Nigeria. Goat meat is used as a primary ingredient, and some versions may use crayfish.",
                            MenuId = 1L,
                            Name = "Goatmeat Pepper Soup",
                            Price = 1500m
                        },
                        new
                        {
                            SubMenuId = 12L,
                            CreatedAt = new DateTime(2020, 3, 27, 5, 48, 18, 791, DateTimeKind.Local).AddTicks(6122),
                            Description = "Nigerian Catfish Pepper Soup (popularly known as Point & Kill) is that Nigerian pepper soup that is made with fresh cat fish.",
                            MenuId = 1L,
                            Name = "Catfish Pepper Soup",
                            Price = 2500m
                        });
                });

            modelBuilder.Entity("MFR.DomainModels.OrderDetail", b =>
                {
                    b.HasOne("MFR.DomainModels.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MFR.DomainModels.SubMenu", "SubMenu")
                        .WithMany()
                        .HasForeignKey("SubMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFR.DomainModels.ShoppingBasketItem", b =>
                {
                    b.HasOne("MFR.DomainModels.SubMenu", "SubMenu")
                        .WithMany()
                        .HasForeignKey("SubMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFR.DomainModels.SubMenu", b =>
                {
                    b.HasOne("MFR.DomainModels.Menu", "Menu")
                        .WithMany("SubMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
