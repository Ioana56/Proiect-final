using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Remastered.Migrations
{
    public partial class Removeimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted",
                table: "posts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted",
                table: "posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
