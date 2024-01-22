using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddSpendingRequestColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequest_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingRequest_Activities_ActivityId",
                table: "SpendingRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingRequest",
                table: "SpendingRequest");

            migrationBuilder.RenameTable(
                name: "SpendingRequest",
                newName: "SpendingRequests");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingRequest_ActivityId",
                table: "SpendingRequests",
                newName: "IX_SpendingRequests_ActivityId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SpendingRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SpendingRequests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "SpendingRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequestorUserId",
                table: "SpendingRequests",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestorUserName",
                table: "SpendingRequests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingRequests",
                table: "SpendingRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingRequests_Activities_ActivityId",
                table: "SpendingRequests",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SpendingRequests_SpendingRequestId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingRequests_Activities_ActivityId",
                table: "SpendingRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingRequests",
                table: "SpendingRequests");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SpendingRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SpendingRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "SpendingRequests");

            migrationBuilder.DropColumn(
                name: "RequestorUserId",
                table: "SpendingRequests");

            migrationBuilder.DropColumn(
                name: "RequestorUserName",
                table: "SpendingRequests");

            migrationBuilder.RenameTable(
                name: "SpendingRequests",
                newName: "SpendingRequest");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingRequests_ActivityId",
                table: "SpendingRequest",
                newName: "IX_SpendingRequest_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingRequest",
                table: "SpendingRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SpendingRequest_SpendingRequestId",
                table: "Expenses",
                column: "SpendingRequestId",
                principalTable: "SpendingRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingRequest_Activities_ActivityId",
                table: "SpendingRequest",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
