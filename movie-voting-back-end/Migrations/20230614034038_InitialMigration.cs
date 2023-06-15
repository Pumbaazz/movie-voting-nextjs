using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movievotingbackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), @"Persistence\Scripts\Movie.sql");
            migrationBuilder.Sql(File.ReadAllText(scriptPath));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Reactions");
        }
    }
}
