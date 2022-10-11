using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class usermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Notes_noteEntityNoteID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_noteEntityNoteID",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "noteEntityNoteID",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_FundooDbTable_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "FundooDbTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_FundooDbTable_UserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.AddColumn<long>(
                name: "noteEntityNoteID",
                table: "Notes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_noteEntityNoteID",
                table: "Notes",
                column: "noteEntityNoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Notes_noteEntityNoteID",
                table: "Notes",
                column: "noteEntityNoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
