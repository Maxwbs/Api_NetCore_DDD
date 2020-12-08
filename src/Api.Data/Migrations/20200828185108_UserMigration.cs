using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    nome = table.Column<string>(maxLength: 60, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ID", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_email",
                table: "USER",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
