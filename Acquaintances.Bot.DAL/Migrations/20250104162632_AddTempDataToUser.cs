using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTempDataToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "photos",
                newName: "FileId");

            migrationBuilder.AddColumn<string>(
                name: "TempDataJson",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempDataJson",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "photos",
                newName: "SourceId");
        }
    }
}
