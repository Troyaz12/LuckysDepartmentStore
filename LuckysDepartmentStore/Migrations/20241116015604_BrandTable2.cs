using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class BrandTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d482fb78-bbb5-4e97-86c2-fc61741606bd", "AQAAAAIAAYagAAAAEBe5e+4F6nLpc6Y4SJAoRXIKohjukMruhSDpVpzgYDL3L8GiF4n8cV8ugwODvJu6bQ==", "f2cd8737-7798-4a2f-baa3-a8f5ce9a267a" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(8334));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(8379));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(9494));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(9505));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 818, DateTimeKind.Local).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2513));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2531));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 56, 3, 819, DateTimeKind.Local).AddTicks(2534));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "235aa96e-4327-4f38-8233-0caf5cb47da3", "AQAAAAIAAYagAAAAEBmBdPyJ24vBhzUBl6OGWDjUIjxlT0ndffTh0LDWDVL6vYrdznlcJxF4px4SHWT18g==", "fd2841f6-d9dc-453c-bad8-b7ab1fd2bb66" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(2732));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(2743));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5913));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5926));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 19, 52, 36, 684, DateTimeKind.Local).AddTicks(5933));
        }
    }
}
