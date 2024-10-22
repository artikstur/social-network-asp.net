using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITISHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentEntity_PostEntity_PostId",
                table: "CommentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentEntity_Users_UserId",
                table: "CommentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntity_Users_UserId",
                table: "PostEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceEntity_PostEntity_PostEntityId",
                table: "ResourceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceEntity",
                table: "ResourceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostEntity",
                table: "PostEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentEntity",
                table: "CommentEntity");

            migrationBuilder.RenameTable(
                name: "ResourceEntity",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "PostEntity",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "CommentEntity",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceEntity_PostEntityId",
                table: "Resources",
                newName: "IX_Resources_PostEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_PostEntity_UserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentEntity_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentEntity_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Posts_PostEntityId",
                table: "Resources",
                column: "PostEntityId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Posts_PostEntityId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "ResourceEntity");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "PostEntity");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CommentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_PostEntityId",
                table: "ResourceEntity",
                newName: "IX_ResourceEntity_PostEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "PostEntity",
                newName: "IX_PostEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "CommentEntity",
                newName: "IX_CommentEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "CommentEntity",
                newName: "IX_CommentEntity_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceEntity",
                table: "ResourceEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostEntity",
                table: "PostEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentEntity",
                table: "CommentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_PostEntity_PostId",
                table: "CommentEntity",
                column: "PostId",
                principalTable: "PostEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_Users_UserId",
                table: "CommentEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntity_Users_UserId",
                table: "PostEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceEntity_PostEntity_PostEntityId",
                table: "ResourceEntity",
                column: "PostEntityId",
                principalTable: "PostEntity",
                principalColumn: "Id");
        }
    }
}
