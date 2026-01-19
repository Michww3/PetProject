using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableUserIdToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_GuidId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_GuidId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "GuidId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GuidId",
                table: "Projects",
                column: "GuidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_GuidId",
                table: "Projects",
                column: "GuidId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
