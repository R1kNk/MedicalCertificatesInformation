using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class MedCertsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CertificateTerm",
                table: "MedicalCertificates",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CertificateTerm",
                table: "MedicalCertificates",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
