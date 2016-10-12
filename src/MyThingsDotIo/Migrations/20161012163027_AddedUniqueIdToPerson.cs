using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyThingsDotIo.Migrations
{
    public partial class AddedUniqueIdToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Users",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Person",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Users");
        }
    }
}
