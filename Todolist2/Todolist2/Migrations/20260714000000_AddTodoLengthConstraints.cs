using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Todolist2.Infrastructure.Data;

#nullable disable

namespace Todolist2.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260714000000_AddTodoLengthConstraints")]
public partial class AddTodoLengthConstraints : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "Todos",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Todos",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "Todos",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Todos",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(500)",
            oldMaxLength: 500);
    }
}
