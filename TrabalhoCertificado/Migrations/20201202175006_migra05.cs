using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class migra05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "TBUsuario");

            migrationBuilder.AddColumn<string>(
                name: "senhaEncry",
                table: "TBUsuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senhaEncry",
                table: "TBUsuario");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "TBUsuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
