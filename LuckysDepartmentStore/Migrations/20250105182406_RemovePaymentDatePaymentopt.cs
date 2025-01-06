using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymentDatePaymentopt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedDate",
                table: "PaymentOptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10334807-ecae-476b-9b00-169afaf1fce5", "AQAAAAIAAYagAAAAEPchrZDqZOHLRNTMgRXeMjQW5e4v4J6uwEaaSDlD1q1ipfhnoj7Kt3e7ymQePKwmFQ==", "fd5de487-ac89-4bda-892e-2de4533eef2f" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(2966));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(3017));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(3019));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(4052));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(4063));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(4065));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7063));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7065));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7067));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7069));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 24, 5, 294, DateTimeKind.Local).AddTicks(7070));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedDate",
                table: "PaymentOptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d9d44f4-aec6-4022-9b4d-eb714dbf7bde", "AQAAAAIAAYagAAAAEGXmmBNnx7C5B5T0d7IwDOHzHfmQ9eMAY2l0KhFAcEclG6Wyr6wxjCzRpTvoj1ArSw==", "6f5a6827-eb0e-4dd3-81c8-1caf7450e714" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(135));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(137));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(1227));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(1240));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4141));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 12, 14, 3, 543, DateTimeKind.Local).AddTicks(4146));
        }
    }
}
