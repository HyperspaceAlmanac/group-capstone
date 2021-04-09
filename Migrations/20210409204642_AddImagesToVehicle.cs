using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class AddImagesToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fuelStart",
                table: "Trips",
                newName: "FuelStart");

            migrationBuilder.RenameColumn(
                name: "fuelEnd",
                table: "Trips",
                newName: "FuelEnd");

            migrationBuilder.AddColumn<string>(
                name: "AfterTripBackImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AfterTripFrontImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AfterTripInteriorBack",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AfterTripInteriorFront",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AfterTripLeftImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AfterTripRightImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripBackImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripFrontImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripInteriorBack",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripInteriorFront",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripLeftImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeforeTripRightImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterTripBackImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AfterTripFrontImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AfterTripInteriorBack",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AfterTripInteriorFront",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AfterTripLeftImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AfterTripRightImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripBackImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripFrontImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripInteriorBack",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripInteriorFront",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripLeftImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BeforeTripRightImage",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "FuelStart",
                table: "Trips",
                newName: "fuelStart");

            migrationBuilder.RenameColumn(
                name: "FuelEnd",
                table: "Trips",
                newName: "fuelEnd");
        }
    }
}
