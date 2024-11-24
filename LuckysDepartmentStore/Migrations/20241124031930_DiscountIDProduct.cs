using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class DiscountIDProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e70e0193-2592-4f8f-834e-cc01873aa86a", "AQAAAAIAAYagAAAAEEQz/Kg78THjcWS0yX33L/smY7JJFtVvqZYovNeCW/C8r6E9OCZg3LxMiKOzw8p2sQ==", "4a9e5ca3-d221-448a-bd49-9295b86598cb" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 25, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 25, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 25, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(869));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(882));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(884));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3788));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 23, 21, 19, 30, 26, DateTimeKind.Local).AddTicks(3811));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7719bc5c-0851-40b0-ad22-258e0c5c6bb1", "AQAAAAIAAYagAAAAEMtKfoyDmu9GitcatdgzwBwPRoPe7845YC2urfuVdrqkYQ3NHLLi0E5R0ZY2JqmP9A==", "1b8704f1-1d5d-4cf9-b9c7-81a09c89b5be" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4681));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4695));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 22, 16, 33, 50, 72, DateTimeKind.Local).AddTicks(4701));
        }
    }
}
