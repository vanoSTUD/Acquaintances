using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStateToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "users");
        }
    }
}
