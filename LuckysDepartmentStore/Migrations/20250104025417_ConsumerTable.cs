using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class ConsumerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(5464));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(5517));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(6776));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(6789));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b1ba98a-b111-4dc3-b374-6b0a1d839917", "AQAAAAIAAYagAAAAEJErkn3wVHXUAQ8pxLmvAAzQOk1bsE+F3EJTjPrp3UvAGdQnjNUCtLu6kK52I/C6Jw==", "efe1a7e0-c532-46ed-8211-36696da9a4aa" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9768));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9784));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9787));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 20, 54, 16, 417, DateTimeKind.Local).AddTicks(9789));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(8562));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(9575));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(9586));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 447, DateTimeKind.Local).AddTicks(9588));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "686a217a-ced6-40cc-9e92-5cff2cdbedef", "AQAAAAIAAYagAAAAEDch3lQgX7RBcRxJOeukln+E7uS32MwD/+wCaQbD7QoWoOPtgOE2pgkWqkA7cpo3eA==", "97c32929-40c9-401a-9348-20222497fab5" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2471));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2486));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2487));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2489));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 19, 48, 57, 448, DateTimeKind.Local).AddTicks(2490));
        }
    }
}
