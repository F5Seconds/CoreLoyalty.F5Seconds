using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class UpdateRelationSanPhamCuaHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuaHangProduct",
                columns: table => new
                {
                    CuaHangsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuaHangProduct", x => new { x.CuaHangsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CuaHangProduct_CuaHangs_CuaHangsId",
                        column: x => x.CuaHangsId,
                        principalTable: "CuaHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuaHangProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CuaHangProduct_ProductsId",
                table: "CuaHangProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuaHangProduct");
        }
    }
}
