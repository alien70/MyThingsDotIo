using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyThingsDotIo.Migrations
{
    public partial class AddedUniqueIdToPerson5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Alias",
                table: "Person",
                column: "Alias",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_Alias",
                table: "Person");

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Person",
                nullable: true);
        }
    }
}
