using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class reqTagsDiscountTagstoDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountTag",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccf78956-c597-4e74-a285-4e3dcc3c0bca", "AQAAAAIAAYagAAAAEAEDF7izyTzGKYZFUq9U6EjoP9fsFvmCWdEGxAxgIQXHAGJykzO1vJnSVzqD8NhliA==", "df7e79b5-7415-4a8a-9641-3fa52076ce32" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 135, DateTimeKind.Local).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3737));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3753));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3756));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3757));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 8, 53, 54, 136, DateTimeKind.Local).AddTicks(3763));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountTag",
                table: "Discounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e56feca3-dfd4-422c-9a8e-adbc7110f3af", "AQAAAAIAAYagAAAAEEQx+aGU6ujeIILE91a/4AnohB0F+vNDHpMSV5VIngRieB89hUi7g4Z2TkpNEz4t/A==", "a408c085-f3aa-4913-99b9-999f66445c2c" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 349, DateTimeKind.Local).AddTicks(9045));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 349, DateTimeKind.Local).AddTicks(9097));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 349, DateTimeKind.Local).AddTicks(9100));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(127));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(139));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3297));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3298));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 23, 17, 57, 350, DateTimeKind.Local).AddTicks(3301));
        }
    }
}
