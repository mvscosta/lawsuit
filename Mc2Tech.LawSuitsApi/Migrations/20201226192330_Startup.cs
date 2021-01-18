using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2Tech.LawSuitsApi.Migrations
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LawSuits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnifiedProcessNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DistributedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientPhysicalFolder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SituationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentLawSuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JusticeSecret = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ExternalReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawSuits", x => x.Id);
                    table.UniqueConstraint("AK_LawSuits_ExternalReference", x => x.ExternalReference);
                    table.ForeignKey(
                        name: "FK_LawSuits_LawSuits_ParentLawSuitId",
                        column: x => x.ParentLawSuitId,
                        principalTable: "LawSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LawSuits_ParentLawSuitId",
                table: "LawSuits",
                column: "ParentLawSuitId");

            migrationBuilder.CreateIndex(
                name: "IX_LawSuits_UnifiedProcessNumber",
                table: "LawSuits",
                column: "UnifiedProcessNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LawSuits");
        }
    }
}
