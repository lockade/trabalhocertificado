using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoAtividade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAtividade = table.Column<string>(maxLength: 100, nullable: false),
                    DescAtividade = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAtividade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBAtividades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    atividadeID = table.Column<int>(nullable: false),
                    descricao = table.Column<string>(maxLength: 250, nullable: true),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    dataFim = table.Column<DateTime>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: true),
                    anexo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBAtividades_TipoAtividade_atividadeID",
                        column: x => x.atividadeID,
                        principalTable: "TipoAtividade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAtividades_atividadeID",
                table: "TBAtividades",
                column: "atividadeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAtividades");

            migrationBuilder.DropTable(
                name: "TipoAtividade");
        }
    }
}
