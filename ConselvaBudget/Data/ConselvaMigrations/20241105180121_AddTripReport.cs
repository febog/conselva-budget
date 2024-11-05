using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddTripReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarriedOutActivities",
                table: "Trips",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContributedResources",
                table: "Trips",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicturesUrl",
                table: "Trips",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualitativeResults",
                table: "Trips",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialResults",
                table: "Trips",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalResults",
                table: "Trips",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarriedOutActivities",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ContributedResources",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PicturesUrl",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "QualitativeResults",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SocialResults",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TechnicalResults",
                table: "Trips");
        }
    }
}
