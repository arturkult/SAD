using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAD.Migrations
{
    public partial class AddAuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_CardOwners_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    RoomId = table.Column<Guid>(nullable: true),
                    Result = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_CardOwners_UserId",
                        column: x => x.UserId,
                        principalTable: "CardOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_RoomId",
                table: "AuditLogs",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_CardOwners_OwnerId",
                table: "Rooms",
                column: "OwnerId",
                principalTable: "CardOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
