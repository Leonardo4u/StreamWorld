using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamWorld.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistDetailsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Artists",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkFoto",
                table: "Artists",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaisOrigem",
                table: "Artists",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "LinkFoto",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "PaisOrigem",
                table: "Artists");
        }
    }
}
