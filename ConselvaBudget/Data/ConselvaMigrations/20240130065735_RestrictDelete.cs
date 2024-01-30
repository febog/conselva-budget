using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class RestrictDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ActivityBudgets_ActivityBudgetId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ActivityBudgets_ActivityBudgetId",
                table: "Expenses",
                column: "ActivityBudgetId",
                principalTable: "ActivityBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ActivityBudgets_ActivityBudgetId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ActivityBudgets_ActivityBudgetId",
                table: "Expenses",
                column: "ActivityBudgetId",
                principalTable: "ActivityBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
