﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationDB.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration30042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpertsID",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpertsID",
                table: "User");
        }
    }
}
