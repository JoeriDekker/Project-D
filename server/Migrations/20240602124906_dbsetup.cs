using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class dbsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: false),
                    details = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControlPC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    secret = table.Column<string>(type: "varchar(255)", nullable: false),
                    meetputBroID = table.Column<string>(type: "varchar(255)", nullable: false),
                    ControlPCSecret = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroundWaterLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    controlPCID = table.Column<string>(type: "varchar(100)", nullable: false),
                    date = table.Column<string>(type: "varchar(100)", nullable: false),
                    level = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroundWaterLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<string>(type: "varchar(100)", nullable: false),
                    controlPCID = table.Column<string>(type: "varchar(100)", nullable: false),
                    controlPCSecret = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    ConfirmationToken = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    actionTypeID = table.Column<int>(type: "integer", nullable: false),
                    dateTimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLog_ActionType_actionTypeID",
                        column: x => x.actionTypeID,
                        principalTable: "ActionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionLog_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaterLevelSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PoleHeight = table.Column<decimal>(type: "decimal", nullable: false),
                    IdealHeight = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterLevelSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaterLevelSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLog_actionTypeID",
                table: "ActionLog",
                column: "actionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLog_userId",
                table: "ActionLog",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterLevelSettings_UserId",
                table: "WaterLevelSettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLog");

            migrationBuilder.DropTable(
                name: "ControlPC");

            migrationBuilder.DropTable(
                name: "GroundWaterLog");

            migrationBuilder.DropTable(
                name: "UserSetting");

            migrationBuilder.DropTable(
                name: "WaterLevelSettings");

            migrationBuilder.DropTable(
                name: "ActionType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
