using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class writer_key_changed_to_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    WriterName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WriterID = table.Column<int>(type: "int", nullable: false),
                    WriterMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.WriterName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Writers");
        }
    }
}
