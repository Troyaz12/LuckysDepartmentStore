using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b5e4d95-4882-4f0f-91eb-a6b1323c6e79", "AQAAAAIAAYagAAAAEPqO4QEPydGoBvtgfXQQgwjVVSXlZxgALFfAmdKvcsaE2u82FPwvEpqMo0QxJobGaw==", "ca15acdc-fa4c-46bb-a3d7-dccbaca9965c" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8605));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8615));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1581));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1586));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1587));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c05a190-e2a8-4698-a122-4baa534ba263", "AQAAAAIAAYagAAAAEMGyssS+4qwQnHO4AaE79pdD5wU/CehrfD52Eu49s0GDN7W+ZleUVzyxkqEoDfqsqw==", "662c2d6b-1293-47a4-b8bf-3fe2735f557d" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2702));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5607));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5628));
        }
    }
}
