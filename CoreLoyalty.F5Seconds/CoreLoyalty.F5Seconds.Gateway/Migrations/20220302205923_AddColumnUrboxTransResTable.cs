using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class AddColumnUrboxTransResTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "UrboxTransactionResponses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CodeDisplay",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CodeDisplayType",
                table: "UrboxTransactionResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeImage",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EstimateDelivery",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeDisplay",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "CodeDisplayType",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "CodeImage",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "EstimateDelivery",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "UrboxTransactionResponses");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "UrboxTransactionResponses");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiryDate",
                table: "UrboxTransactionResponses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
