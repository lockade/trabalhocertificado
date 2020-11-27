using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class alteracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaConfirmar",
                table: "TBUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenhaConfirmar",
                table: "TBUsuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
