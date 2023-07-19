using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    /// <inheritdoc />
    public partial class AddModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountCategory_AccountCategoryId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_BusinessSubprogram_BusinessSubprogramId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSubprogram_BusinessPrograms_BusinessProgramId",
                table: "BusinessSubprogram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessSubprogram",
                table: "BusinessSubprogram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountCategory",
                table: "AccountCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "BusinessSubprogram",
                newName: "BusinessSubprograms");

            migrationBuilder.RenameTable(
                name: "AccountCategory",
                newName: "AccountCategories");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSubprogram_BusinessProgramId",
                table: "BusinessSubprograms",
                newName: "IX_BusinessSubprograms_BusinessProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_BusinessSubprogramId",
                table: "Accounts",
                newName: "IX_Accounts_BusinessSubprogramId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_AccountCategoryId",
                table: "Accounts",
                newName: "IX_Accounts_AccountCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessSubprograms",
                table: "BusinessSubprograms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountCategories",
                table: "AccountCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Segment = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityBudgets_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityBudgets_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityBudgetId = table.Column<int>(type: "int", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ActivityBudgets_ActivityBudgetId",
                        column: x => x.ActivityBudgetId,
                        principalTable: "ActivityBudgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ResultId",
                table: "Activities",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityBudgets_AccountId",
                table: "ActivityBudgets",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityBudgets_ActivityId",
                table: "ActivityBudgets",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ActivityBudgetId",
                table: "Expenses",
                column: "ActivityBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ProjectId",
                table: "Results",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountCategories_AccountCategoryId",
                table: "Accounts",
                column: "AccountCategoryId",
                principalTable: "AccountCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BusinessSubprograms_BusinessSubprogramId",
                table: "Accounts",
                column: "BusinessSubprogramId",
                principalTable: "BusinessSubprograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSubprograms_BusinessPrograms_BusinessProgramId",
                table: "BusinessSubprograms",
                column: "BusinessProgramId",
                principalTable: "BusinessPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountCategories_AccountCategoryId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BusinessSubprograms_BusinessSubprogramId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSubprograms_BusinessPrograms_BusinessProgramId",
                table: "BusinessSubprograms");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ActivityBudgets");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessSubprograms",
                table: "BusinessSubprograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountCategories",
                table: "AccountCategories");

            migrationBuilder.RenameTable(
                name: "BusinessSubprograms",
                newName: "BusinessSubprogram");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameTable(
                name: "AccountCategories",
                newName: "AccountCategory");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSubprograms_BusinessProgramId",
                table: "BusinessSubprogram",
                newName: "IX_BusinessSubprogram_BusinessProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_BusinessSubprogramId",
                table: "Account",
                newName: "IX_Account_BusinessSubprogramId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountCategoryId",
                table: "Account",
                newName: "IX_Account_AccountCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessSubprogram",
                table: "BusinessSubprogram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountCategory",
                table: "AccountCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AccountCategory_AccountCategoryId",
                table: "Account",
                column: "AccountCategoryId",
                principalTable: "AccountCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_BusinessSubprogram_BusinessSubprogramId",
                table: "Account",
                column: "BusinessSubprogramId",
                principalTable: "BusinessSubprogram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSubprogram_BusinessPrograms_BusinessProgramId",
                table: "BusinessSubprogram",
                column: "BusinessProgramId",
                principalTable: "BusinessPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
