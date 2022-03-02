using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class EditAndAddColumnUrboxTransResTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "UrboxTransactionResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "UrboxTransactionResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "UrboxTransactionResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "UrboxTransactionResponses",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "UrboxTransactionResponses");
        }
    }
}
