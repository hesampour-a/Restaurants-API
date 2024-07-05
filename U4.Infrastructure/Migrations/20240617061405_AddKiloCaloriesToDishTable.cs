﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace U4.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKiloCaloriesToDishTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KiloCalories",
                table: "Dishes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KiloCalories",
                table: "Dishes");
        }
    }
}
