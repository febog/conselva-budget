using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddDeleteBehaviors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityBudgets_AccountAssignments_AccountAssignmentId",
                table: "ActivityBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityBudgets_Activities_ActivityId",
                table: "ActivityBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityBudgets_AccountAssignments_AccountAssignmentId",
                table: "ActivityBudgets",
                column: "AccountAssignmentId",
                principalTable: "AccountAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityBudgets_Activities_ActivityId",
                table: "ActivityBudgets",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityBudgets_AccountAssignments_AccountAssignmentId",
                table: "ActivityBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityBudgets_Activities_ActivityId",
                table: "ActivityBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityBudgets_AccountAssignments_AccountAssignmentId",
                table: "ActivityBudgets",
                column: "AccountAssignmentId",
                principalTable: "AccountAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityBudgets_Activities_ActivityId",
                table: "ActivityBudgets",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
