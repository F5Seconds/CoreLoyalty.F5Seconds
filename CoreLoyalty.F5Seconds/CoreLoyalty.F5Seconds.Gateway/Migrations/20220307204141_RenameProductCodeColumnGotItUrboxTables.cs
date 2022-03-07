using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class RenameProductCodeColumnGotItUrboxTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropductCode",
                table: "UrboxTransactionResponses",
                newName: "ProductCode");

            migrationBuilder.RenameColumn(
                name: "PropductCode",
                table: "GotItTransactionResponses",
                newName: "ProductCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "UrboxTransactionResponses",
                newName: "PropductCode");

            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "GotItTransactionResponses",
                newName: "PropductCode");
        }
    }
}
