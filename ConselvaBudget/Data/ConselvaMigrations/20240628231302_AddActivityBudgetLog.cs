using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddActivityBudgetLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityBudgetLogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityBudgetId = table.Column<int>(type: "int", nullable: false),
                    EventAuthorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityBudgetLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityBudgetLogEntries_ActivityBudgets_ActivityBudgetId",
                        column: x => x.ActivityBudgetId,
                        principalTable: "ActivityBudgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityBudgetLogEntries_ActivityBudgetId",
                table: "ActivityBudgetLogEntries",
                column: "ActivityBudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityBudgetLogEntries");
        }
    }
}
