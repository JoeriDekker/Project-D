using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddedHouseNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Addresses");
        }
    }
}
