using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStaticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5da88e70-967b-4737-91b1-dbcc850b5f73"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[] { new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"), "admin@motorrent.com", "Administrator", "$2a$11$kt3sttPZjh6qXrlOL9UPh.JeigGW4LwMuJw5W87LfrMXDO.vqobhy", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[] { new Guid("5da88e70-967b-4737-91b1-dbcc850b5f73"), "admin@motorrent.com", "Administrator", "$2a$11$W9QbLEfbts8yEsNGX6a8U.DoAcnykyMhXDR6F0smrK0oRgkq5xdGu", 0 });
        }
    }
}
