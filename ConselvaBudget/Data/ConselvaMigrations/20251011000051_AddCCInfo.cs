using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddCCInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditCardEnding",
                table: "ExpenseInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardIssuingBank",
                table: "ExpenseInvoices",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardEnding",
                table: "ExpenseInvoices");

            migrationBuilder.DropColumn(
                name: "CreditCardIssuingBank",
                table: "ExpenseInvoices");
        }
    }
}
