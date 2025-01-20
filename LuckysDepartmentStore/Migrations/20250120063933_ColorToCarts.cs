using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class ColorToCarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea141593-56e9-4081-a74d-294236d0fc53", "AQAAAAIAAYagAAAAELVykYuEYYKoxEvYLBfRsMu5YywP4NJr+qW6i4J49W20n24E455Q0SzWWi9LUu/tow==", "752080b8-0fab-496f-89a3-103d0f296735" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2677));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5229));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1529));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1544));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1563));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Carts");

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
    }
}
