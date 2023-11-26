using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Bank.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fDInterestRatesTable",
                columns: table => new
                {
                    FDInterestRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinDuration = table.Column<int>(type: "int", nullable: false),
                    MaxDuration = table.Column<int>(type: "int", nullable: false),
                    ROI = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fDInterestRatesTable", x => x.FDInterestRateId);
                });

            migrationBuilder.CreateTable(
                name: "rolesTable",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolesTable", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "usersTable",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersTable", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_usersTable_rolesTable_RoleId",
                        column: x => x.RoleId,
                        principalTable: "rolesTable",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adminsTable",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminsTable", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_adminsTable_usersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "usersTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customersTable",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails = table.Column<double>(type: "float", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customersTable", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_customersTable_usersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "usersTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accountsTable",
                columns: table => new
                {
                    AccountNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountBalance = table.Column<double>(type: "float", nullable: false),
                    IntrestRate = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountsTable", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_accountsTable_customersTable_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customersTable",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documentsTable",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentsTable", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_documentsTable_customersTable_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customersTable",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "queryTable",
                columns: table => new
                {
                    QueryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueryText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplyQuery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueryStatus = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queryTable", x => x.QueryId);
                    table.ForeignKey(
                        name: "FK_queryTable_customersTable_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customersTable",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fDAccountsTable",
                columns: table => new
                {
                    FDAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ROI = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaturityAmount = table.Column<double>(type: "float", nullable: false),
                    InterestReturns = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fDAccountsTable", x => x.FDAccountId);
                    table.ForeignKey(
                        name: "FK_fDAccountsTable_accountsTable_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accountsTable",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactionsTable",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionAmount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBalance = table.Column<double>(type: "float", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionsTable", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_transactionsTable_accountsTable_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accountsTable",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accountsTable_CustomerId",
                table: "accountsTable",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_adminsTable_UserId",
                table: "adminsTable",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customersTable_UserId",
                table: "customersTable",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documentsTable_CustomerId",
                table: "documentsTable",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_fDAccountsTable_AccountId",
                table: "fDAccountsTable",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_queryTable_CustomerId",
                table: "queryTable",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_transactionsTable_AccountId",
                table: "transactionsTable",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTable_RoleId",
                table: "usersTable",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminsTable");

            migrationBuilder.DropTable(
                name: "documentsTable");

            migrationBuilder.DropTable(
                name: "fDAccountsTable");

            migrationBuilder.DropTable(
                name: "fDInterestRatesTable");

            migrationBuilder.DropTable(
                name: "queryTable");

            migrationBuilder.DropTable(
                name: "transactionsTable");

            migrationBuilder.DropTable(
                name: "accountsTable");

            migrationBuilder.DropTable(
                name: "customersTable");

            migrationBuilder.DropTable(
                name: "usersTable");

            migrationBuilder.DropTable(
                name: "rolesTable");
        }
    }
}
