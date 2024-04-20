using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunFox.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingAuditableInEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Enrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Enrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedTime",
                table: "Enrollments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Enrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Enrollments");
        }
    }
}
