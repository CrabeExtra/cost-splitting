using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_2.Database.Migrations
{
    /// <inheritdoc />
    public partial class names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contributions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Name",
                table: "Expenses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_Name",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contributions");
        }
    }
}
