using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameDueTimeToDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueTime",
                table: "TodoItems",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "TodoItems",
                newName: "DueTime");
        }
    }
}
