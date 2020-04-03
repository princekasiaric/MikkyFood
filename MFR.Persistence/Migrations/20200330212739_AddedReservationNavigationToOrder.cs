using Microsoft.EntityFrameworkCore.Migrations;

namespace MFR.Persistence.Migrations
{
    public partial class AddedReservationNavigationToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReservationId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                table: "Orders",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Orders");
        }
    }
}
