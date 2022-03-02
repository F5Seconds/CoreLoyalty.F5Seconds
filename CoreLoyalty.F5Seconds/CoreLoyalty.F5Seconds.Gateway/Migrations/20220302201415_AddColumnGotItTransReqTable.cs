using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class AddColumnGotItTransReqTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "GotItTransactionResponses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "GotItTransactionResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VoucherImageLink",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VoucherLink",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VoucherLinkCode",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "VoucherImageLink",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "VoucherLink",
                table: "GotItTransactionResponses");

            migrationBuilder.DropColumn(
                name: "VoucherLinkCode",
                table: "GotItTransactionResponses");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiryDate",
                table: "GotItTransactionResponses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
