using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedIsDeletedPropertyandsetDefaultvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ToDo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 19, 21, 41, 29, 61, DateTimeKind.Utc).AddTicks(2682), new DateTime(2020, 4, 19, 21, 41, 29, 61, DateTimeKind.Utc).AddTicks(5164) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ToDo");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 19, 19, 52, 40, 637, DateTimeKind.Utc).AddTicks(4982), new DateTime(2020, 4, 19, 19, 52, 40, 637, DateTimeKind.Utc).AddTicks(7795) });
        }
    }
}
