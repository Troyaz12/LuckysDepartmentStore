using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class BrandTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BrandID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7989d3e-b752-4799-83d5-a9a10dee970a", "AQAAAAIAAYagAAAAEAzjyHxF0CYuZ+BtJv2Xwjy7C6uhzMSNwA/HgxH1HD8EjG6Zzlh92vQgbJ221e3NPw==", "fce3ff65-2399-4cc1-983a-fd61fdd5f084" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6970));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8081));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1260));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1274));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1276));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1277));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1280));
        }
    }
}
