using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class updateCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CertificateTerm",
                table: "MedicalCertificates",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CertificateTerm",
                table: "MedicalCertificates",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
