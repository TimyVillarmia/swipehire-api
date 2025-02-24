using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class MergedInternEducationAndWorkToIntern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternEducationMapping");

            migrationBuilder.DropTable(
                name: "InternWorkExperiences");

            migrationBuilder.DropTable(
                name: "InternEducations");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Interns");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Interns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyLocation",
                table: "Interns",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Interns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Interns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndWorkDate",
                table: "Interns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Interns",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Interns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Interns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartWorkDate",
                table: "Interns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "CompanyLocation",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "EndWorkDate",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "StartWorkDate",
                table: "Interns");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Interns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InternEducations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    School = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternEducations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternWorkExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternId = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternWorkExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternWorkExperiences_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternEducationMapping",
                columns: table => new
                {
                    InternEducationId = table.Column<int>(type: "int", nullable: false),
                    InternId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternEducationMapping", x => new { x.InternEducationId, x.InternId });
                    table.ForeignKey(
                        name: "FK_InternEducationMapping_InternEducations_InternEducationId",
                        column: x => x.InternEducationId,
                        principalTable: "InternEducations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternEducationMapping_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternEducationMapping_InternId",
                table: "InternEducationMapping",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_InternWorkExperiences_InternId",
                table: "InternWorkExperiences",
                column: "InternId");
        }
    }
}
