using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeamViewPoC.Data.Migrations
{
    public partial class _0519182023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNote_Projects_ProjectId",
                table: "ProjectNote");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNote_WorkItems_WorkItemId",
                table: "ProjectNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectNote",
                table: "ProjectNote");

            migrationBuilder.DropIndex(
                name: "IX_ProjectNote_WorkItemId",
                table: "ProjectNote");

            migrationBuilder.DropColumn(
                name: "WorkItemId",
                table: "ProjectNote");

            migrationBuilder.RenameTable(
                name: "ProjectNote",
                newName: "ProjectNotes");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNote_ProjectId",
                table: "ProjectNotes",
                newName: "IX_ProjectNotes_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectNotes",
                table: "ProjectNotes",
                column: "ProjectNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectId",
                table: "ProjectNotes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNotes_Projects_ProjectId",
                table: "ProjectNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectNotes",
                table: "ProjectNotes");

            migrationBuilder.RenameTable(
                name: "ProjectNotes",
                newName: "ProjectNote");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectNotes_ProjectId",
                table: "ProjectNote",
                newName: "IX_ProjectNote_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "WorkItemId",
                table: "ProjectNote",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectNote",
                table: "ProjectNote",
                column: "ProjectNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNote_WorkItemId",
                table: "ProjectNote",
                column: "WorkItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNote_Projects_ProjectId",
                table: "ProjectNote",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNote_WorkItems_WorkItemId",
                table: "ProjectNote",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
