using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    public partial class UpdateRelationshipTransTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRequests_TransactionResFails_TransactionResFailId",
                table: "TransactionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRequests_TransactionResponses_TransactionResponse~",
                table: "TransactionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRequests_TransactionResFailId",
                table: "TransactionRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRequests_TransactionResponseId",
                table: "TransactionRequests");

            migrationBuilder.DropColumn(
                name: "TransactionResFailId",
                table: "TransactionRequests");

            migrationBuilder.DropColumn(
                name: "TransactionResponseId",
                table: "TransactionRequests");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TransactionResFailId",
                table: "TransactionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionResponseId",
                table: "TransactionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_TransactionResFailId",
                table: "TransactionRequests",
                column: "TransactionResFailId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_TransactionResponseId",
                table: "TransactionRequests",
                column: "TransactionResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRequests_TransactionResFails_TransactionResFailId",
                table: "TransactionRequests",
                column: "TransactionResFailId",
                principalTable: "TransactionResFails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRequests_TransactionResponses_TransactionResponse~",
                table: "TransactionRequests",
                column: "TransactionResponseId",
                principalTable: "TransactionResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
