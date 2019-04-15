using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class AddManagerDepartmentUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentManagerId",
                table: "Departments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentManagerId",
                table: "Departments",
                column: "DepartmentManagerId",
                unique: true,
                filter: "[DepartmentManagerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_DepartmentManagerId",
                table: "Departments",
                column: "DepartmentManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_DepartmentManagerId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
