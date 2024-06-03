using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class RenameRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLogEntries_SpendingRequests_ExpenseRequestId",
                table: "RequestLogEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_SpendingRequests_SpendingRequestId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "SpendingRequests");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RequestorUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ActivityId",
                table: "Requests",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Requests_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLogEntries_Requests_ExpenseRequestId",
                table: "RequestLogEntries",
                column: "ExpenseRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Requests_SpendingRequestId",
                table: "Trips",
                column: "SpendingRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Requests_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLogEntries_Requests_ExpenseRequestId",
                table: "RequestLogEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Requests_SpendingRequestId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.CreateTable(
                name: "SpendingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RequestorUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendingRequests_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpendingRequests_ActivityId",
                table: "SpendingRequests",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLogEntries_SpendingRequests_ExpenseRequestId",
                table: "RequestLogEntries",
                column: "ExpenseRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_SpendingRequests_SpendingRequestId",
                table: "Trips",
                column: "SpendingRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
