using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_profiles_ProfileId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_profiles_ProfileId",
                table: "users",
                column: "ProfileId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_profiles_ProfileId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_profiles_ProfileId",
                table: "users",
                column: "ProfileId",
                principalTable: "profiles",
                principalColumn: "Id");
        }
    }
}
