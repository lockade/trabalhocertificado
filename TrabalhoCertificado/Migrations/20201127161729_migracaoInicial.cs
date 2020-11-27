using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class migracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBUsuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    SenhaConfirmar = table.Column<string>(nullable: false),
                    nome = table.Column<string>(maxLength: 250, nullable: false),
                    previlegios = table.Column<string>(nullable: true),
                    ativado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBUsuario", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBUsuario");
        }
    }
}
