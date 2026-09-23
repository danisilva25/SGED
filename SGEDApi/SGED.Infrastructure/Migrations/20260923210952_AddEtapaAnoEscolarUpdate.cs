using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEtapaAnoEscolarUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disciplinas");

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "EtapaAnoEscolar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Modalidade",
                table: "EtapaAnoEscolar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "EtapaAnoEscolar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "EtapaAnoEscolar");

            migrationBuilder.DropColumn(
                name: "Modalidade",
                table: "EtapaAnoEscolar");

            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "EtapaAnoEscolar");

            migrationBuilder.CreateTable(
                name: "disciplinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplinas", x => x.Id);
                });
        }
    }
}
