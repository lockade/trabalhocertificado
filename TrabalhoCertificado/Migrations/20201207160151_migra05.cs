using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class migra05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBRecuperarSenhaLinks",
                columns: table => new
                {
                    IDEncry = table.Column<string>(nullable: false),
                    tempo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBRecuperarSenhaLinks", x => x.IDEncry);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBRecuperarSenhaLinks");
        }
    }
}
