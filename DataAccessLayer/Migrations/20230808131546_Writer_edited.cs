using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Writer_edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterAbout",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "WriterImage",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "WriterPassword",
                table: "Writers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterAbout",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WriterImage",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WriterPassword",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
