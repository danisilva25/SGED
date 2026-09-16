using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "disciplinas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "disciplinas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "disciplinas",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "disciplinas");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "disciplinas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "disciplinas");
        }
    }
}
