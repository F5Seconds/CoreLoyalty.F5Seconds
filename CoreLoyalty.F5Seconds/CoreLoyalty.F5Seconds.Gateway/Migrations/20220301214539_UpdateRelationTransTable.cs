using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class UpdateRelationTransTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionResFails_TransactionRequests_TransactionRequestId",
                table: "TransactionResFails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionResponses_TransactionRequests_TransactionRequestId",
                table: "TransactionResponses");

            migrationBuilder.DropIndex(
                name: "IX_TransactionResponses_TransactionRequestId",
                table: "TransactionResponses");

            migrationBuilder.DropIndex(
                name: "IX_TransactionResFails_TransactionRequestId",
                table: "TransactionResFails");

            migrationBuilder.DropColumn(
                name: "TransactionRequestId",
                table: "TransactionResponses");

            migrationBuilder.DropColumn(
                name: "TransactionRequestId",
                table: "TransactionResFails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionRequestId",
                table: "TransactionResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionRequestId",
                table: "TransactionResFails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionResponses_TransactionRequestId",
                table: "TransactionResponses",
                column: "TransactionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionResFails_TransactionRequestId",
                table: "TransactionResFails",
                column: "TransactionRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionResFails_TransactionRequests_TransactionRequestId",
                table: "TransactionResFails",
                column: "TransactionRequestId",
                principalTable: "TransactionRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionResponses_TransactionRequests_TransactionRequestId",
                table: "TransactionResponses",
                column: "TransactionRequestId",
                principalTable: "TransactionRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
