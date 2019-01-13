using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class SecondNameAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Students");
        }
    }
}
