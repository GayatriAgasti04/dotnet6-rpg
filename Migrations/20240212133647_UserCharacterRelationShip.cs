﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet6rpg.Migrations
{
    /// <inheritdoc />
    public partial class UserCharacterRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_characters_UserId",
                table: "characters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_UserId",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "characters");
        }
    }
}
