using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwaWallet.Entity.Migrations
{
    public partial class Recordadd_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Records",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Records",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_Date",
                table: "Records",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Records_Description",
                table: "Records",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Records_Tag",
                table: "Records",
                column: "Tag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_Date",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_Description",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_Tag",
                table: "Records");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Records",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Records",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
