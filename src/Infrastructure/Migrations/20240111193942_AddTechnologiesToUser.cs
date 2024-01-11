using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoduTeam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnologiesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Technologies",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Technologies",
                table: "AspNetUsers");
        }
    }
}
