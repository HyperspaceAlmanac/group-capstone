using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalService.Migrations
{
    public partial class moreseeding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/images/08q1/267367/2008-toyota-tacoma-photo-193561-s-original.jpg?fill=2:1&resize=1200:*");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://cdn1.mecum.com/auctions/pa0715/pa0715-216861/images/pa0715-216861_2@2x.jpg?1436997552000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://www.google.com/imgres?imgurl=https%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fimages%3Furl%3Dhttp%3A%2F%2Fsmartcdn.prod.postmedia.digital%2Fdriving%2Fwp-content%2Fuploads%2F2013%2F08%2F85428491.jpg%26w%3D580%26h%3D370&imgrefurl=https%3A%2F%2Fdriving.ca%2Ftoyota%2Ftacoma%2Freviews%2Froad-test%2Froad-test-2008-toyota-tacoma-2&tbnid=wEkshGvejDsKKM&vet=12ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ..i&docid=ZDXPJovseuFbHM&w=580&h=370&q=2008%20toyota%20tacoma&ved=2ahUKEwj-ytXB9vnvAhXBDlMKHYImAzgQMygBegUIARDlAQ");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://www.google.com/url?sa=i&url=http%3A%2F%2Fdavidsclassiccars.com%2Fchevrolet%2F24214-1979-chevrolet-blazer-k5-4x4-lifted-rebuilt-350-v8-auto-custom-deluxe-nt-bronco.html&psig=AOvVaw3kFOxYaZnHGAr3Tld8YuN6&ust=1618358915386000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLjNxff2-e8CFQAAAAAdAAAAABAD");
        }
    }
}
