using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class CartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "816c81c0-17fb-430a-98c1-daa7c3d8f66d", "AQAAAAIAAYagAAAAEHw+NKhfjfz2AjbTq5oTp/MyJ2dc8FXz4Ink3kATdMX6PIT82p4sL5WyUVQBhEt2zQ==", "d877c975-99f5-4c58-9147-7b844675b4a7" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 200, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 200, DateTimeKind.Local).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 200, DateTimeKind.Local).AddTicks(8863));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(623));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(654));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(656));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4064));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4066));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4069));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 11, 5, 201, DateTimeKind.Local).AddTicks(4070));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0942d3d1-0d2c-41d9-b13f-6246fc301266", "AQAAAAIAAYagAAAAEDBdoEyLn/MTykluI4+ERdk0fQS9YyBqUkr0YoNJ5MnSZKYgDIp4SB+kYjaduw0jTA==", "497e7717-9326-441b-820c-7b74b3c6f902" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3963));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3964));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5056));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7999));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(8001));
        }
    }
}
