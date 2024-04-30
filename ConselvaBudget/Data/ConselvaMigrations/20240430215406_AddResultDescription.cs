using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddResultDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Results",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Results");
        }
    }
}
