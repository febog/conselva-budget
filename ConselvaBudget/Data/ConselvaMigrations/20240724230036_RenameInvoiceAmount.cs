using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class RenameInvoiceAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceAmount",
                table: "ExpenseInvoices",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ExpenseInvoices",
                newName: "InvoiceAmount");
        }
    }
}
