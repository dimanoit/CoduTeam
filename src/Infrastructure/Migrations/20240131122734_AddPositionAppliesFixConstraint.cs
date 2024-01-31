using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoduTeam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionAppliesFixConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionApplies_Positions_UserId",
                table: "PositionApplies");

            migrationBuilder.CreateIndex(
                name: "IX_PositionApplies_PositionId",
                table: "PositionApplies",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionApplies_Positions_PositionId",
                table: "PositionApplies",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionApplies_Positions_PositionId",
                table: "PositionApplies");

            migrationBuilder.DropIndex(
                name: "IX_PositionApplies_PositionId",
                table: "PositionApplies");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionApplies_Positions_UserId",
                table: "PositionApplies",
                column: "UserId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
