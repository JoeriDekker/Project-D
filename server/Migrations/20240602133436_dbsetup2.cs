using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class dbsetup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterLevelSettings_Users_UserId",
                table: "WaterLevelSettings");

            migrationBuilder.DropIndex(
                name: "IX_WaterLevelSettings_UserId",
                table: "WaterLevelSettings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "WaterLevelSettings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "WaterLevelSettingsId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WaterLevelSettingsId",
                table: "Users",
                column: "WaterLevelSettingsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WaterLevelSettings_WaterLevelSettingsId",
                table: "Users",
                column: "WaterLevelSettingsId",
                principalTable: "WaterLevelSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WaterLevelSettings_WaterLevelSettingsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WaterLevelSettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WaterLevelSettingsId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "WaterLevelSettings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterLevelSettings_UserId",
                table: "WaterLevelSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterLevelSettings_Users_UserId",
                table: "WaterLevelSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
