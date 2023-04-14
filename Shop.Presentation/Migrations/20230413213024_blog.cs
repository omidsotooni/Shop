using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Persistence.Migrations
{
    public partial class blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 14, 1, 0, 24, 314, DateTimeKind.Local).AddTicks(9474));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 14, 1, 0, 24, 314, DateTimeKind.Local).AddTicks(9523));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 14, 1, 0, 24, 314, DateTimeKind.Local).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 14, 1, 0, 24, 314, DateTimeKind.Local).AddTicks(9551));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 13, 14, 28, 14, 302, DateTimeKind.Local).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 13, 14, 28, 14, 302, DateTimeKind.Local).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 13, 14, 28, 14, 302, DateTimeKind.Local).AddTicks(7255));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 13, 14, 28, 14, 302, DateTimeKind.Local).AddTicks(7265));
        }
    }
}
