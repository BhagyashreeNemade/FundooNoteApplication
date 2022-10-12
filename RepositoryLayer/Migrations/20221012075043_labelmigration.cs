using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class labelmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabelsTable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false),
                    userEntityUserId = table.Column<long>(nullable: true),
                    noteEntityNoteID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelsTable", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_LabelsTable_Notes_noteEntityNoteID",
                        column: x => x.noteEntityNoteID,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabelsTable_FundooDbTable_userEntityUserId",
                        column: x => x.userEntityUserId,
                        principalTable: "FundooDbTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelsTable_noteEntityNoteID",
                table: "LabelsTable",
                column: "noteEntityNoteID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelsTable_userEntityUserId",
                table: "LabelsTable",
                column: "userEntityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelsTable");
        }
    }
}
