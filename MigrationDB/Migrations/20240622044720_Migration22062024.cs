using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationDB.Migrations
{
    /// <inheritdoc />
    public partial class Migration22062024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponses_SubscriptionsId",
                table: "PaymentResponses",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponses_UsersId",
                table: "PaymentResponses",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentResponses_Subscription_SubscriptionsId",
                table: "PaymentResponses",
                column: "SubscriptionsId",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentResponses_User_UsersId",
                table: "PaymentResponses",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentResponses_Subscription_SubscriptionsId",
                table: "PaymentResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentResponses_User_UsersId",
                table: "PaymentResponses");

            migrationBuilder.DropIndex(
                name: "IX_PaymentResponses_SubscriptionsId",
                table: "PaymentResponses");

            migrationBuilder.DropIndex(
                name: "IX_PaymentResponses_UsersId",
                table: "PaymentResponses");
        }
    }
}
