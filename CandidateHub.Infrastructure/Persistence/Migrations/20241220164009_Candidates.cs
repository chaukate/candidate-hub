using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Candidates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CallTimeInterval = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    LinkedInUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    GitHubUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
