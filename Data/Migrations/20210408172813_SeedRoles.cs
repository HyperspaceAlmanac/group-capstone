using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0f3f8561-ba6a-4530-b5e4-9e66425089de", "a0b6873b-bcfd-4880-8987-3a403620f5f2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff2a00f0-9394-4ebf-9546-135cd2ab6500", "5d137252-b432-4cba-a5ec-6b1150addd1a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "135fc814-e4c8-4423-b037-6b28a1eb96d0", "3a9c7601-e621-457d-8f50-7572109c09a7", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f3f8561-ba6a-4530-b5e4-9e66425089de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "135fc814-e4c8-4423-b037-6b28a1eb96d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff2a00f0-9394-4ebf-9546-135cd2ab6500");
        }
    }
}
