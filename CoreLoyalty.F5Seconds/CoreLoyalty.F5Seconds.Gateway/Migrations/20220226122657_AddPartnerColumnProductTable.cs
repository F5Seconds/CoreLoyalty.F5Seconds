using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class AddPartnerColumnProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Partner",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Partner",
                table: "Products");
        }
    }
}
