using MFR.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace MFR.Persistence
{
    public static class MFRDbSeeder
    {
        public static void SeededMenu(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasData(
                    new Menu
                    {
                        MenuId = 1,
                        Name = "African Starter",
                        Image = null,
                        CreatedAt = DateTime.Now,
                    },
                    new Menu
                    {
                        MenuId = 2,
                        Name = "Protein",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    { 
                        MenuId = 3,
                        Name = "Swallow",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 4,
                        Name = "Soup",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    { 
                        MenuId = 5,
                        Name = "Sides & Salad",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 6,
                        Name = "Porridge",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 7,
                        Name = "Rice Dish",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 8,
                        Name = "Desert",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 9,
                        Name = "Chef's Special",
                        Image = null,
                        CreatedAt = DateTime.Now
                    },
                    new Menu
                    {
                        MenuId = 10,
                        Name = "Beverages",
                        Image = null,
                        CreatedAt = DateTime.Now
                    }); 
            });
        }
        
        public static void SeededSubMenu(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubMenu>(entity =>
            {
                entity.HasData(
                    new SubMenu
                    {
                        SubMenuId = 1,
                        MenuId = 9,
                        Name = "Seafood Okoro",
                        Description = "Seafood okro is a delightful and delicious mix of all your favourite seafood and okro.",
                        Image = null,
                        Price = 2500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    { 
                        SubMenuId = 2,
                        MenuId = 9,
                        Name = "Ofe Nsala",
                        Description = "Ofe Nsala (Nsala Soup) is a soup popular in the eastern part of Nigeria. " +
                        "It is also known as ''white soup'' nsala soup is know for its light texture.",
                        Image = null,
                        Price = 1500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 3,
                        MenuId = 9,
                        Name = "Abacha & Ugba",
                        Description = "Abacha and Ugba also known as African salad is a Cassava based dish from the Igbo " +
                        "tribe of Eastern Nigeria.",
                        Image = null,
                        Price = 1500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 4,
                        MenuId = 9,
                        Name = "Bitterleaf",
                        Description = "This bitter leaf soup recipe (also know as Ofe Onugbu) is generously stocked with " +
                        "flavoursome meats, fish and cocoyams. Make it when you’re in the mood for something warm, serve with" +
                        " your favourite swallow, tuck in and enjoy.",
                        Image = null,
                        Price = 1000m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 5,
                        MenuId = 4,
                        Name = "Ogbono",
                        Description = " A tasty and hearty Nigerian soup made from Ogbono seeds (bush mango seeds) added with " +
                        "pre-cooked meat.",
                        Image = null,
                        Price = 500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    { 
                        SubMenuId = 6,
                        MenuId = 4,
                        Name = "Efo riro",
                        Description = "The African Stewed Spinach also popularly known as Efo riro is a one-pot stew with layers" +
                        " of flavor.",
                        Image = null,
                        Price = 500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 7,
                        MenuId = 3,
                        Name = "Pounded Yam",
                        Description = "Pounded Yam is one of the best Nigeria swallows that is eaten with the various delicious " +
                        "Nigerian soups. it is made with white boiled yam.",
                        Image = null,
                        Price = 250m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 8,
                        MenuId = 3,
                        Name = "Amala",
                        Description = "Àmàlà is a local indigenous Nigerian food, native to the Yoruba tribe in the South-western " +
                        "parts of the country.",
                        Image = null,
                        Price = 150m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    { 
                        SubMenuId = 9,
                        MenuId = 2,
                        Name = "Pepper Gizzard",
                        Description = "Peppered Gizzard is simply Nigerian stewed gizzards.",
                        Image = null,
                        Price = 500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 10,
                        MenuId = 2,
                        Name = "Goat Meat",
                        Description = "Goat meat or goat's meat is the meat of the domestic goat.",
                        Image = null,
                        Price = 250m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 11,
                        MenuId = 1,
                        Name = "Goatmeat Pepper Soup",
                        Description = "Goat meat pepper soup, also referred to as nwo-nwo, ngwo-ngwo, and goat pepper soup, " +
                        "is a soup in Nigeria. Goat meat is used as a primary ingredient, and some versions may use crayfish.",
                        Image = null,
                        Price = 1500m,
                        CreatedAt = DateTime.Now
                    },
                    new SubMenu
                    {
                        SubMenuId = 12,
                        MenuId = 1,
                        Name = "Catfish Pepper Soup",
                        Description = "Nigerian Catfish Pepper Soup (popularly known as Point & Kill) is that Nigerian pepper " +
                        "soup that is made with fresh cat fish.",
                        Image = null,
                        Price = 2500m,
                        CreatedAt = DateTime.Now
                    });
            });
        }

        public static void SeededRole(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasData(
                    new IdentityRole
                    {
                        Name = "Visitor",
                        NormalizedName = "VISITOR"
                    },
                    new IdentityRole
                    {
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    });
            });
        }
    }
}
