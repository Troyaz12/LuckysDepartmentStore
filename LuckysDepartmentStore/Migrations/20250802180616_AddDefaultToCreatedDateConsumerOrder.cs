using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultToCreatedDateConsumerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerOrders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4dd4f64-4a9b-4a34-8e9c-6369a0c1ed18", "AQAAAAIAAYagAAAAEAF3DXhYFRLFc23e4xBDVSoYnRw0XYSpItPY7mm5v8/qRUcEx7sKK/AH9HBCulS7YA==", "c1b369b9-3e49-4faa-90d0-b83b00f02612" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomerOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSDATETIME()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12aeb4e7-4700-4e0e-865a-4b7f3b745213", "AQAAAAIAAYagAAAAEGAheg4M2nmpfzvLhJOTykdY7hV5i6Y7NsHgK/z0BGfgLf4Gbbu320jPM9Ns+FfZMw==", "9dffb85d-4572-465b-a417-ac40c5d673f2" });
        }
    }
}
