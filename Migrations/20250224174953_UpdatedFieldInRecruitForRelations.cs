using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFieldInRecruitForRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "Recruits");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "Recruits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruits_FieldId",
                table: "Recruits",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruits_Fields_FieldId",
                table: "Recruits",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruits_Fields_FieldId",
                table: "Recruits");

            migrationBuilder.DropIndex(
                name: "IX_Recruits_FieldId",
                table: "Recruits");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Recruits");

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "Recruits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
