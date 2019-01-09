using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCertificates.Repositories.Migrations
{
    public partial class UserChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CuratorId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "CuratorId",
                table: "Groups",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CuratorId",
                table: "Groups",
                newName: "IX_Groups_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Groups",
                newName: "CuratorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ApplicationUserId",
                table: "Groups",
                newName: "IX_Groups_CuratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_CuratorId",
                table: "Groups",
                column: "CuratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
