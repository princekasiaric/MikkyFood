using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MFR.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Location = table.Column<string>(maxLength: 15, nullable: false),
                    OrderTotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    OrderMethod = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    DateOrdered = table.Column<DateTime>(name: "Date Ordered", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfPeople = table.Column<int>(nullable: false),
                    DateBooked = table.Column<DateTime>(name: "Date Booked", nullable: false),
                    Time = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubMenus",
                columns: table => new
                {
                    SubMenuId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenus", x => x.SubMenuId);
                    table.ForeignKey(
                        name: "FK_SubMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(nullable: false),
                    SubMenuId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_SubMenus_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenus",
                        principalColumn: "SubMenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBasketItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubMenuId = table.Column<long>(nullable: false),
                    ShoppingBasketId = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBasketItems_SubMenus_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenus",
                        principalColumn: "SubMenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SubMenuId",
                table: "OrderDetails",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasketItems_SubMenuId",
                table: "ShoppingBasketItems",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenus_MenuId",
                table: "SubMenus",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ShoppingBasketItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SubMenus");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
