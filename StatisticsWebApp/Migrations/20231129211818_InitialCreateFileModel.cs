using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatisticsWebApp.Migrations
{
    public partial class InitialCreateFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "UploadDateTime",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "LinesCount",
                table: "Statistics",
                newName: "FileId");

            migrationBuilder.CreateTable(
                name: "FileModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinesCount = table.Column<int>(type: "int", nullable: false),
                    UploadDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_FileId",
                table: "Statistics",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_FileModel_FileId",
                table: "Statistics",
                column: "FileId",
                principalTable: "FileModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_FileModel_FileId",
                table: "Statistics");

            migrationBuilder.DropTable(
                name: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_FileId",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Statistics",
                newName: "LinesCount");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
