using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "profiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "profiles");
        }
    }
}
