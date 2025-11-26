using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamWorld.Migrations
{
    /// <inheritdoc />
    public partial class AddDiretorToProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiretorId",
                table: "Productions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productions_DiretorId",
                table: "Productions",
                column: "DiretorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Artists_DiretorId",
                table: "Productions",
                column: "DiretorId",
                principalTable: "Artists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Artists_DiretorId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_DiretorId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DiretorId",
                table: "Productions");
        }
    }
}
