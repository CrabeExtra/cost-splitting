using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_2.Database.Migrations
{
    /// <inheritdoc />
    public partial class calculations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributionCents",
                table: "Contributions");

            migrationBuilder.AddColumn<int>(
                name: "CostCents",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "Contributions",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetCostCents",
                table: "Contributions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Contributions",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCents",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "TargetCostCents",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Contributions");

            migrationBuilder.AddColumn<int>(
                name: "ContributionCents",
                table: "Contributions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
