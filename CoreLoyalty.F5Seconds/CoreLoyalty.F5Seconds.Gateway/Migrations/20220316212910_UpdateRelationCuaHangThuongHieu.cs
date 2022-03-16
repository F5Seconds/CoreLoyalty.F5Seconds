using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class UpdateRelationCuaHangThuongHieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThuongHieus_CuaHangs_CuaHangId",
                table: "ThuongHieus");

            migrationBuilder.DropIndex(
                name: "IX_ThuongHieus_CuaHangId",
                table: "ThuongHieus");

            migrationBuilder.DropColumn(
                name: "CuaHangId",
                table: "ThuongHieus");

            migrationBuilder.CreateTable(
                name: "CuaHangThuongHieu",
                columns: table => new
                {
                    CuaHangsId = table.Column<int>(type: "int", nullable: false),
                    ThuongHieusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuaHangThuongHieu", x => new { x.CuaHangsId, x.ThuongHieusId });
                    table.ForeignKey(
                        name: "FK_CuaHangThuongHieu_CuaHangs_CuaHangsId",
                        column: x => x.CuaHangsId,
                        principalTable: "CuaHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuaHangThuongHieu_ThuongHieus_ThuongHieusId",
                        column: x => x.ThuongHieusId,
                        principalTable: "ThuongHieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CuaHangThuongHieu_ThuongHieusId",
                table: "CuaHangThuongHieu",
                column: "ThuongHieusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuaHangThuongHieu");

            migrationBuilder.AddColumn<int>(
                name: "CuaHangId",
                table: "ThuongHieus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuongHieus_CuaHangId",
                table: "ThuongHieus",
                column: "CuaHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuongHieus_CuaHangs_CuaHangId",
                table: "ThuongHieus",
                column: "CuaHangId",
                principalTable: "CuaHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
