using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Common.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class updateModelRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UsersUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogsBlogId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UsersUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogsBlogId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UsersUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UsersUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogsBlogId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogsBlogId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Blogs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogsBlogId",
                table: "Comments",
                column: "BlogsBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UsersUserId",
                table: "Comments",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UsersUserId",
                table: "Blogs",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UsersUserId",
                table: "Blogs",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogsBlogId",
                table: "Comments",
                column: "BlogsBlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UsersUserId",
                table: "Comments",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
