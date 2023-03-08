using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyDBSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TaskItems_TaskItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskItemsStatus_TaskItemStatusId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TaskItemId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TaskItemStatusId",
                table: "TaskItems",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_TaskItemStatusId",
                table: "TaskItems",
                newName: "IX_TaskItems_StatusId");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CommentId",
                table: "TaskItems",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Comments_CommentId",
                table: "TaskItems",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_TaskItemsStatus_StatusId",
                table: "TaskItems",
                column: "StatusId",
                principalTable: "TaskItemsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Comments_CommentId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskItemsStatus_StatusId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_CommentId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "TaskItems",
                newName: "TaskItemStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_StatusId",
                table: "TaskItems",
                newName: "IX_TaskItems_TaskItemStatusId");

            migrationBuilder.AddColumn<int>(
                name: "TaskItemId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaskItemId",
                table: "Comments",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TaskItems_TaskItemId",
                table: "Comments",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_TaskItemsStatus_TaskItemStatusId",
                table: "TaskItems",
                column: "TaskItemStatusId",
                principalTable: "TaskItemsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
