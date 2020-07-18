using Microsoft.EntityFrameworkCore.Migrations;

namespace MFR.Persistence.Migrations
{
    public partial class RemovedSubMenuIdItemFromShoppingBasketItem_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBasketItems_SubMenus_SubMenuId",
                table: "ShoppingBasketItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1784c5a0-c7f8-42c9-97a2-02acb3c873e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29cce73f-facf-41f9-8284-7fc9ea0d5030");

            migrationBuilder.AlterColumn<long>(
                name: "SubMenuId",
                table: "ShoppingBasketItems",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89d00041-2c07-45ee-b5fb-1b258a04225f", "269d4e34-4a37-4d3d-b65d-58d0f923bbb9", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "837307fc-b7aa-4acd-a93e-b1f45d61d9b2", "5edac7c3-9208-478b-ae9b-42e8f8b97793", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBasketItems_SubMenus_SubMenuId",
                table: "ShoppingBasketItems",
                column: "SubMenuId",
                principalTable: "SubMenus",
                principalColumn: "SubMenuId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBasketItems_SubMenus_SubMenuId",
                table: "ShoppingBasketItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "837307fc-b7aa-4acd-a93e-b1f45d61d9b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89d00041-2c07-45ee-b5fb-1b258a04225f");

            migrationBuilder.AlterColumn<long>(
                name: "SubMenuId",
                table: "ShoppingBasketItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1784c5a0-c7f8-42c9-97a2-02acb3c873e0", "ada12968-3af1-41ec-96a3-2199f60e7944", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29cce73f-facf-41f9-8284-7fc9ea0d5030", "3816023e-374a-4273-912f-b148d8b11cd5", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBasketItems_SubMenus_SubMenuId",
                table: "ShoppingBasketItems",
                column: "SubMenuId",
                principalTable: "SubMenus",
                principalColumn: "SubMenuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
