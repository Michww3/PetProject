using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProject.Migrations
{
    /// <inheritdoc />
    public partial class renameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JSONData",
                table: "NodeGraphs",
                newName: "JsonData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonData",
                table: "NodeGraphs",
                newName: "JSONData");
        }
    }
}
