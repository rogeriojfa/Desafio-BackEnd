using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deliveryman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Deliverymen_DeliverymanId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motos_MotoId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DeliverymanId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotoId",
                table: "Rentals");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_Cnpj",
                table: "Deliverymen",
                column: "CNPJ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deliverymen_Cnpj",
                table: "Deliverymen");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliverymanId",
                table: "Rentals",
                column: "DeliverymanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotoId",
                table: "Rentals",
                column: "MotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Deliverymen_DeliverymanId",
                table: "Rentals",
                column: "DeliverymanId",
                principalTable: "Deliverymen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motos_MotoId",
                table: "Rentals",
                column: "MotoId",
                principalTable: "Motos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
