using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliverymen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cnh = table.Column<string>(type: "text", nullable: false),
                    CnhType = table.Column<int>(type: "integer", nullable: false),
                    CNHImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverymen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Plate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliverymanId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotoId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PlanDays = table.Column<int>(type: "integer", nullable: false),
                    DailyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Deliverymen_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalTable: "Deliverymen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Motos_MotoId",
                        column: x => x.MotoId,
                        principalTable: "Motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_Cnh",
                table: "Deliverymen",
                column: "Cnh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_Cnpj",
                table: "Deliverymen",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motos_Plate",
                table: "Motos",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliverymanId",
                table: "Rentals",
                column: "DeliverymanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotoId",
                table: "Rentals",
                column: "MotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Deliverymen");

            migrationBuilder.DropTable(
                name: "Motos");
        }
    }
}
