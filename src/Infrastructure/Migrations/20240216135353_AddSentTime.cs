using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoduTeam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSentTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentTime",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "Messages");
        }
    }
}
