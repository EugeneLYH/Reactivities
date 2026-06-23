using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FollowerEntityAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId1",
                table: "UserFollowings");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowings_TargetId1",
                table: "UserFollowings");

            migrationBuilder.DropColumn(
                name: "TargetId1",
                table: "UserFollowings");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings",
                column: "ObserverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings");

            migrationBuilder.AddColumn<string>(
                name: "TargetId1",
                table: "UserFollowings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowings_TargetId1",
                table: "UserFollowings",
                column: "TargetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId1",
                table: "UserFollowings",
                column: "TargetId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
