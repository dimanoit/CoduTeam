using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoduTeam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntroducePositionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionStatus",
                table: "Positions",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Positions",
                newName: "PositionStatus");
        }
    }
}
