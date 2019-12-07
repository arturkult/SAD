using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAD.Migrations
{
    public partial class AddCardOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardOwnerId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardOwners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardOwnerId",
                table: "Cards",
                column: "CardOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardOwners_CardOwnerId",
                table: "Cards",
                column: "CardOwnerId",
                principalTable: "CardOwners",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardOwners_CardOwnerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_CardOwners_OwnerId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "CardOwners");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardOwnerId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CardOwnerId",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
