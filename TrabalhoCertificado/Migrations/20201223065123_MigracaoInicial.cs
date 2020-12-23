using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBAtividades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    idTipoAtiv = table.Column<int>(nullable: false),
                    descricao = table.Column<string>(maxLength: 250, nullable: true),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    dataFim = table.Column<DateTime>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: true),
                    anexo = table.Column<string>(nullable: true),
                    idUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividades", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBTiposAtividades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAtividade = table.Column<string>(maxLength: 100, nullable: false),
                    DescAtividade = table.Column<string>(maxLength: 250, nullable: true),
                    idUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTiposAtividades", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAtividades");

            migrationBuilder.DropTable(
                name: "TBTiposAtividades");
        }
    }
}
