using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class Renames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Projects",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Donors",
                newName: "ShortName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Donors",
                newName: "Name");
        }
    }
}
