using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInternFieldRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interns_FieldId",
                table: "Interns");

            migrationBuilder.CreateIndex(
                name: "IX_Interns_FieldId",
                table: "Interns",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interns_FieldId",
                table: "Interns");

            migrationBuilder.CreateIndex(
                name: "IX_Interns_FieldId",
                table: "Interns",
                column: "FieldId",
                unique: true,
                filter: "[FieldId] IS NOT NULL");
        }
    }
}
