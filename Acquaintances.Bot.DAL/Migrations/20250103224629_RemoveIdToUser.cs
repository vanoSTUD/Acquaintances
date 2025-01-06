using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_users_RecipientId",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_reciprocities_users_RecipientId",
                table: "reciprocities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_ChatId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_users_RecipientId",
                table: "likes",
                column: "RecipientId",
                principalTable: "users",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reciprocities_users_RecipientId",
                table: "reciprocities",
                column: "RecipientId",
                principalTable: "users",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_users_RecipientId",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_reciprocities_users_RecipientId",
                table: "reciprocities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_ChatId",
                table: "users",
                column: "ChatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_users_RecipientId",
                table: "likes",
                column: "RecipientId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_photos_users_OwnerId",
                table: "photos",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reciprocities_users_RecipientId",
                table: "reciprocities",
                column: "RecipientId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
