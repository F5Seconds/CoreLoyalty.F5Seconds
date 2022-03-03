using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class AddColumnGotItTransResTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateText",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsedBrand",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsedTime",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateText",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "UsedBrand",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "UsedTime",
                table: "GotItTransactionResponses");
        }
    }
}
