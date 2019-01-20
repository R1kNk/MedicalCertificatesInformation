using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class HospitalDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalCertificates_Hospitals_HospitalId",
                table: "MedicalCertificates");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_MedicalCertificates_HospitalId",
                table: "MedicalCertificates");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "MedicalCertificates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "MedicalCertificates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_HospitalId",
                table: "MedicalCertificates",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_Name",
                table: "Hospitals",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalCertificates_Hospitals_HospitalId",
                table: "MedicalCertificates",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
