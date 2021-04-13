using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class moreseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CurrentCity", "CurrentState", "CurrentStreet", "CurrentZip", "Distance", "Duration", "Fuel", "Image", "IsAvailable", "IsOperational", "LastKnownLatitude", "LastKnownLongitude", "Location", "Make", "Model", "Odometer", "Year" },
                values: new object[,]
                {
                    { 3, "Chula Vista", "CA", "1120 Cuyamaca Avenue", "91911", null, null, 100, "https://www.google.com/imgres?imgurl=https%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fimages%3Furl%3Dhttp%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fwp-content%2Fuploads%2F2013%2F08%2F85428491.jpg%26w%3D580%26h%3D370&imgrefurl=https%3A%2F%2Fdriving.ca%2Ftoyota%2Ftacoma%2Freviews%2Froad-test%2Froad-test-2008-toyota-tacoma-2&tbnid=wEkshGvejDsKKM&vet=12ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ..i&docid=ZDXPJovseuFbHM&w=580&h=370&q=2008%20toyota%20tacoma&ved=2ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ", true, true, null, null, null, "Toyota", "Tacoma", 101011, 2008 },
                    { 4, "San Diego", "CA", "9449 Friars Road", "92108", null, null, 100, "https://www.google.com/url?sa=i&url=http%3A%2F%2Fdavidsclassiccars.com%2Fchevrolet%2F24214-1979-chevrolet-blazer-k5-4x4-lifted-rebuilt-350-v8-auto-custom-deluxe-nt-bronco.html&psig=AOvVaw3kFOxYaZnHGAr3Tld8YuN6&ust=1618358915386000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLjNxff2-e8CFQAAAAAdAAAAABAD", true, true, null, null, null, "Chevrolet", "Bronco", 90011, 1979 },
                    { 5, "Chula Vista", "CA", "740 Hilltop Drive", "91910", null, null, 100, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/2019-nissan-altima-102-1538074559.jpg?crop=0.822xw:1.00xh;0.138xw,0&resize=640:*", true, true, null, null, null, "Nissan", "Altima", 90011, 2021 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vehicles");
        }
    }
}
