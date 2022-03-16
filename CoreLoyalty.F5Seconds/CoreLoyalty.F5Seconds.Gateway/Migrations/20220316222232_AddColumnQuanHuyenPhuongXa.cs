using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class AddColumnQuanHuyenPhuongXa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenDayDu",
                table: "QuanHuyens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenDayDu",
                table: "PhuongXas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenDayDu",
                table: "QuanHuyens");

            migrationBuilder.DropColumn(
                name: "TenDayDu",
                table: "PhuongXas");
        }
    }
}
