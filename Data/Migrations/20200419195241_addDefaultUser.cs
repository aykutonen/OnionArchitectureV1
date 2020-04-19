using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "Status", "UpdatedAt" },
                values: new object[] { 1L, new DateTime(2020, 4, 19, 19, 52, 40, 637, DateTimeKind.Utc).AddTicks(4982), "aykutonen@gmail.com", "Aykut", "Önen", "123", 1, new DateTime(2020, 4, 19, 19, 52, 40, 637, DateTimeKind.Utc).AddTicks(7795) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "id",
                keyValue: 1L);
        }
    }
}
