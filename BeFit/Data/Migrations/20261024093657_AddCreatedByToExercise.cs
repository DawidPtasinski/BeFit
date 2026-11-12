using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Exercise",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_CreatedByID",
                table: "Exercise",
                column: "CreatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_AspNetUsers_CreatedByID",
                table: "Exercise",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_AspNetUsers_CreatedByID",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_CreatedByID",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Exercise");
        }
    }
}
