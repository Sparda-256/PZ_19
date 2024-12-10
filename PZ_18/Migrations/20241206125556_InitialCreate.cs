using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PZ_18.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplianceCategories",
                columns: table => new
                {
                    ApplianceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplianceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplianceCategories", x => x.ApplianceID);
                });

            migrationBuilder.CreateTable(
                name: "UserCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_UserCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "UserCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplianceID = table.Column<int>(type: "int", nullable: false),
                    ApplianceModel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResolutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TechnicianID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_RepairRequests_ApplianceCategories_ApplianceID",
                        column: x => x.ApplianceID,
                        principalTable: "ApplianceCategories",
                        principalColumn: "ApplianceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequests_Accounts_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_RepairRequests_Accounts_TechnicianID",
                        column: x => x.TechnicianID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianID = table.Column<int>(type: "int", nullable: true),
                    RequestID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Notes_RepairRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RepairRequests",
                        principalColumn: "RequestID");
                    table.ForeignKey(
                        name: "FK_Notes_Accounts_TechnicianID",
                        column: x => x.TechnicianID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "RepairComponents",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    ComponentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ComponentQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairComponents", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_RepairComponents_RepairRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RepairRequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TechnicianID",
                table: "Notes",
                column: "TechnicianID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_RequestID",
                table: "Notes",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairComponents_RequestID",
                table: "RepairComponents",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_CustomerID",
                table: "RepairRequests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_TechnicianID",
                table: "RepairRequests",
                column: "TechnicianID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_ApplianceID",
                table: "RepairRequests",
                column: "ApplianceID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CategoryID",
                table: "Accounts",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Notes");
            migrationBuilder.DropTable(name: "RepairComponents");
            migrationBuilder.DropTable(name: "RepairRequests");
            migrationBuilder.DropTable(name: "ApplianceCategories");
            migrationBuilder.DropTable(name: "Accounts");
            migrationBuilder.DropTable(name: "UserCategories");
        }
    }
}
