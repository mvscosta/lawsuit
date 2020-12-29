using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2Tech.LawSuitsApi.Migrations.SituationDb
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Situations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ExternalReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situations", x => x.Id);
                    table.UniqueConstraint("AK_Situations_ExternalReference", x => x.ExternalReference);
                });

            migrationBuilder.InsertData(
                table: "Situations",
                columns: new[] { "Id", "CreationUserId", "Description", "ExternalReference", "IsClosed", "IsSystemDefined", "ModifiedUserId", "Name" },
                values: new object[,]
                {
                    { new Guid("2349b7e9-f966-4159-8b8b-08d8a4840d3f"), null, null, new Guid("e276a54c-a260-4e62-dac0-08d8ac2522da"), true, false, null, "Finalizado" },
                    { new Guid("fea5977d-d634-475c-0a87-08d8a484dd71"), null, null, new Guid("a8251c77-f129-4043-dac1-08d8ac2522da"), false, false, null, "Arquivado" },
                    { new Guid("12cfd7d7-e71a-43c9-07e2-08d8a4850f73"), null, null, new Guid("ee070fa6-bb2b-4452-dac2-08d8ac2522da"), false, false, null, "Em Recurso" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), null, null, new Guid("a6110fb7-e5b9-4655-dac3-08d8ac2522da"), false, false, null, "Em Andamento" },
                    { new Guid("e0cc0cac-c0fb-4403-8e58-d48d279d50b0"), null, null, new Guid("500d8db6-8fb3-4c7c-dac4-08d8ac2522da"), true, false, null, "Desmembrado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Situations_Name",
                table: "Situations",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Situations");
        }
    }
}
