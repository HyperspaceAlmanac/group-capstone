using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class JWTripIsPaidBoolAdds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Trips");
        }
    }
}
