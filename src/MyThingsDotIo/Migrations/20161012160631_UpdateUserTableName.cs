using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyThingsDotIo.Migrations
{
    public partial class UpdateUserTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Person",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Users",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Person");
        }
    }
}
