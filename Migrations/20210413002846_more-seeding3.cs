using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class moreseeding3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://bringatrailer.com/wp-content/uploads/2017/03/58c1cafa21bcb_143322.jpg?fit=940%2C598");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://car-from-uk.com/ebay/carphotos/full/ebay657229.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://s.aolcdn.com/commerce/autodata/images/CAB00FOT111B0101.jpg");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://cdn1.mecum.com/auctions/ch1014/ch1014-195726/images/ch1014-195726_9@2x.jpg?1412794670000");
        }
    }
}
