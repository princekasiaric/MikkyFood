using Microsoft.EntityFrameworkCore.Migrations;

namespace MFR.Persistence.Migrations
{
    public partial class SeededMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Image", "Name" },
                values: new object[,]
                {
                    { 1L, null, "African Starter" },
                    { 2L, null, "Protein" },
                    { 3L, null, "Swallow" },
                    { 4L, null, "Soup" },
                    { 5L, null, "Sides & Salad" },
                    { 6L, null, "Porridge" },
                    { 7L, null, "Rice Dish" },
                    { 8L, null, "Desert" },
                    { 9L, null, "Chef's Special" },
                    { 10L, null, "Beverages" }
                });

            migrationBuilder.InsertData(
                table: "SubMenus",
                columns: new[] { "SubMenuId", "Description", "Image", "MenuId", "Name", "Price" },
                values: new object[,]
                {
                    { 11L, "Goat meat pepper soup, also referred to as nwo-nwo, ngwo-ngwo, and goat pepper soup, is a soup in Nigeria. Goat meat is used as a primary ingredient, and some versions may use crayfish.", null, 1L, "Goatmeat Pepper Soup", 1500m },
                    { 12L, "Nigerian Catfish Pepper Soup (popularly known as Point & Kill) is that Nigerian pepper soup that is made with fresh cat fish.", null, 1L, "Catfish Pepper Soup", 2500m },
                    { 9L, "Peppered Gizzard is simply Nigerian stewed gizzards.", null, 2L, "Pepper Gizzard", 500m },
                    { 10L, "Goat meat or goat's meat is the meat of the domestic goat.", null, 2L, "Goat Meat", 250m },
                    { 7L, "Pounded Yam is one of the best Nigeria swallows that is eaten with the various delicious Nigerian soups. it is made with white boiled yam.", null, 3L, "Pounded Yam", 250m },
                    { 8L, "Àmàlà is a local indigenous Nigerian food, native to the Yoruba tribe in the South-western parts of the country.", null, 3L, "Amala", 150m },
                    { 5L, " A tasty and hearty Nigerian soup made from Ogbono seeds (bush mango seeds) added with pre-cooked meat.", null, 4L, "Ogbono", 500m },
                    { 6L, "The African Stewed Spinach also popularly known as Efo riro is a one-pot stew with layers of flavor.", null, 4L, "Efo riro", 500m },
                    { 1L, "Seafood okro is a delightful and delicious mix of all your favourite seafood and okro.", null, 9L, "Seafood Okoro", 2500m },
                    { 2L, "Ofe Nsala (Nsala Soup) is a soup popular in the eastern part of Nigeria. It is also known as ''white soup'' nsala soup is know for its light texture.", null, 9L, "Ofe Nsala", 1500m },
                    { 3L, "Abacha and Ugba also known as African salad is a Cassava based dish from the Igbo tribe of Eastern Nigeria.", null, 9L, "Abacha & Ugba", 1500m },
                    { 4L, "This bitter leaf soup recipe (also know as Ofe Onugbu) is generously stocked with flavoursome meats, fish and cocoyams. Make it when you’re in the mood for something warm, serve with your favourite swallow, tuck in and enjoy.", null, 9L, "Bitterleaf", 1000m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "SubMenus",
                keyColumn: "SubMenuId",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 9L);
        }
    }
}
