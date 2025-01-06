using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionSymbolsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "profiles",
                type: "nvarchar(900)",
                maxLength: 900,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "profiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(900)",
                oldMaxLength: 900);
        }
    }
}
