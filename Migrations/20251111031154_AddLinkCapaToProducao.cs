using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamWorld.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkCapaToProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkCapa",
                table: "Productions",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkCapa",
                table: "Productions");
        }
    }
}
