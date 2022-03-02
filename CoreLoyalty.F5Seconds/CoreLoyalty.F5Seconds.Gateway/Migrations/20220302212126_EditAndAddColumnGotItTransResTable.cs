using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class EditAndAddColumnGotItTransResTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "UrboxTransactionResponses",
                newName: "Pin");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNote",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryNote",
                table: "UrboxTransactionResponses");

            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "UrboxTransactionResponses",
                newName: "Description");
        }
    }
}
