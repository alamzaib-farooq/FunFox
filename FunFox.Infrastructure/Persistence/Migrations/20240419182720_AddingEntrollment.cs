using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunFox.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingEntrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollment",
                newName: "IX_Enrollment_CourseId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Enrollment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedTime",
                table: "Enrollment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Enrollment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
