using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChageNameTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Staff_SupervisorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskItemsStatus_TaskItemStatusId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TaskItems");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TaskItemStatusId",
                table: "TaskItems",
                newName: "IX_TaskItems_TaskItemStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SupervisorId",
                table: "TaskItems",
                newName: "IX_TaskItems_SupervisorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TaskItems_TaskItemId",
                table: "Comments",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Staff_SupervisorId",
                table: "TaskItems",
                column: "SupervisorId",
                principalTable: "Staff",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TaskItems_TaskItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Staff_SupervisorId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskItemsStatus_TaskItemStatusId",
                table: "TaskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_TaskItemStatusId",
                table: "Tasks",
                newName: "IX_Tasks_TaskItemStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_SupervisorId",
                table: "Tasks",
                newName: "IX_Tasks_SupervisorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskItemId",
                table: "Comments",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Staff_SupervisorId",
                table: "Tasks",
                column: "SupervisorId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskItemsStatus_TaskItemStatusId",
                table: "Tasks",
                column: "TaskItemStatusId",
                principalTable: "TaskItemsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
