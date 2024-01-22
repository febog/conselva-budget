using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddSpendingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpendingRequestId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SpendingRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendingRequest_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SpendingRequest_ActivityId",
                table: "SpendingRequest",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequest_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequest_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "SpendingRequest");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "SpendingRequestId",
                table: "Expenses");
        }
    }
}
