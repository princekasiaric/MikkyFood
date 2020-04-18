using Microsoft.EntityFrameworkCore.Migrations;

namespace MFR.Persistence.Migrations
{
    public partial class Added_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1784c5a0-c7f8-42c9-97a2-02acb3c873e0", "ada12968-3af1-41ec-96a3-2199f60e7944", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29cce73f-facf-41f9-8284-7fc9ea0d5030", "3816023e-374a-4273-912f-b148d8b11cd5", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1784c5a0-c7f8-42c9-97a2-02acb3c873e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29cce73f-facf-41f9-8284-7fc9ea0d5030");
        }
    }
}
