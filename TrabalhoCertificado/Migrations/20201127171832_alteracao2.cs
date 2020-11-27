using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoCertificado.Migrations
{
    public partial class alteracao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TBUsuario",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TBUsuario_Email",
                table: "TBUsuario",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBUsuario_Email",
                table: "TBUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TBUsuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
