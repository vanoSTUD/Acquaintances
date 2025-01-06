using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PhotosToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_profiles_OwnerId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "photos",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_OwnerId",
                table: "photos",
                newName: "IX_photos_ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_profiles_ProfileId",
                table: "photos",
                column: "ProfileId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_profiles_ProfileId",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "photos",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_ProfileId",
                table: "photos",
                newName: "IX_photos_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_profiles_OwnerId",
                table: "photos",
                column: "OwnerId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
