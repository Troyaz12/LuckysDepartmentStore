using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class DiscountIDCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Discounts",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Discounts",
                newName: "BrandID");

            migrationBuilder.RenameColumn(
                name: "SubCategory",
                table: "Discounts",
                newName: "SubCategoryID");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Discounts",
                newName: "CategoryID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc19c6eb-470b-4035-96c4-ac9b346c70ff", "AQAAAAIAAYagAAAAEKp6BgG5LVg9+bh08qYabxDjuVp27aJvjo8CUbTS1Z3W2GSiEpc/N5Uyl5pDeVTHkw==", "1f9ba5e0-3fac-4470-96c3-14bf221459bb" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(7166));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(8259));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 582, DateTimeKind.Local).AddTicks(8272));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1204));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1219));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1223));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1224));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 35, 2, 583, DateTimeKind.Local).AddTicks(1226));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Discounts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Discounts",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "Discounts",
                newName: "SubCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Discounts",
                newName: "Category");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "164395f1-06f3-46a9-a46a-4f64c5a0ab1d", "AQAAAAIAAYagAAAAEAwBvU5W5EQd7V2+F/Fg7tb1VIVGAs84A6ZNMGhTx76jtf1P35QFrVVkGs/5FXCMDA==", "e1e91431-3a69-42b5-8f41-cd0a28576417" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(3317));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(3319));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(4312));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7284));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7304));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 6, 23, 18, 28, 708, DateTimeKind.Local).AddTicks(7307));
        }
    }
}
