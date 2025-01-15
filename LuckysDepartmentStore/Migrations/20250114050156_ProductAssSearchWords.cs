using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class ProductAssSearchWords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchWords",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb5ad753-2444-4657-b393-9465070806b0", "AQAAAAIAAYagAAAAEHi7Fd3Ogpyf/YrVLBCT1/xXQ6ywCRqS29yIiDNrNKsQm61T7fcAxa3bXqYqOwrbKQ==", "4ed5cf2c-c6e5-467d-b17e-396bfea92bac" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(157));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(206));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4267));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 23, 1, 55, 525, DateTimeKind.Local).AddTicks(4270));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchWords",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c6db61-86aa-4a45-998b-b08d42d85aad", "AQAAAAIAAYagAAAAEAdk6H4YEbyIJLN9kNfYuvVpFBUMIWE856g1ouMN3n3fHAiG5FDP7N8xX3rvPCmnSw==", "a9f2cf3d-6e96-434f-8849-23d730751d12" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(288));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(291));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4605));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4610));
        }
    }
}
