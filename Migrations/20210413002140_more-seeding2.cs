using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class moreseeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://dealeraccelerate-all.s3.amazonaws.com/adrenalin/images/3/5/6/356/87792652e1c0_hd_2000-ford-f-150-harley-davidson-limited-edition.jpg");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "http://car-from-uk.com/ebay/carphotos/full/ebay657234.jpg");
        }
    }
}
