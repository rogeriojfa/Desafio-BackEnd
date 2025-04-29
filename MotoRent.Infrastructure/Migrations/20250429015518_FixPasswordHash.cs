using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"),
                column: "PasswordHash",
                value: "$2a$11$kt3sttPZjh6qXrlOL9UPh.JeigGW4LwMuJw5W87LfrMXDO.vqobhy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"),
                column: "PasswordHash",
                value: "$2a$11$AJ3reorEaQyOf1IPZtJYPOwBZUU2yUb4KxFAjbpZ8mEQnq.DGrGve");
        }
    }
}
