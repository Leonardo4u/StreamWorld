using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamWorld.Migrations
{
    /// <inheritdoc />
    public partial class AddAnoCriacaoResumoToProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoCriacao",
                table: "Productions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resumo",
                table: "Productions",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoCriacao",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Resumo",
                table: "Productions");
        }
    }
}
