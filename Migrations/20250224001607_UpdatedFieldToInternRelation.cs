using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFieldToInternRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternFieldMapping");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "Interns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interns_FieldId",
                table: "Interns",
                column: "FieldId",
                unique: true,
                filter: "[FieldId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Interns_Fields_FieldId",
                table: "Interns",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interns_Fields_FieldId",
                table: "Interns");

            migrationBuilder.DropIndex(
                name: "IX_Interns_FieldId",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Interns");

            migrationBuilder.CreateTable(
                name: "InternFieldMapping",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    InternId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternFieldMapping", x => new { x.FieldId, x.InternId });
                    table.ForeignKey(
                        name: "FK_InternFieldMapping_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternFieldMapping_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternFieldMapping_InternId",
                table: "InternFieldMapping",
                column: "InternId");
        }
    }
}
