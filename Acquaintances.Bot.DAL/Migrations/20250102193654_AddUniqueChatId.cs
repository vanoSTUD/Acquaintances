using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueChatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_ChatId",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_ChatId",
                table: "users",
                column: "ChatId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_ChatId",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_ChatId",
                table: "users",
                column: "ChatId");
        }
    }
}
