using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddColorProductForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Men's products.", "Men's" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Women's products.", "Women's" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Boys youths", "Boys" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryDescription", "CategoryName", "CreatedDate" },
                values: new object[,]
                {
                    { 4, "Girls youths", "Girls", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Boys infant products", "Boys Infant", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Girls infant products", "Girls Infant", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Jeans", "Jeans" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Shirts", "Shirts" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Shoes", "Shoes" });

            migrationBuilder.CreateIndex(
                name: "IX_ColorProducts_ProductID",
                table: "ColorProducts",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorProducts_Products_ProductID",
                table: "ColorProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorProducts_Products_ProductID",
                table: "ColorProducts");

            migrationBuilder.DropIndex(
                name: "IX_ColorProducts_ProductID",
                table: "ColorProducts");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Jeans", "Jeans" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Shirts", "Shirts" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CategoryDescription", "CategoryName" },
                values: new object[] { "Shoes", "Shoes" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Mens products.", "Mens" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Womens products.", "Womens" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                columns: new[] { "SubCategoryDescription", "SubCategoryName" },
                values: new object[] { "Boys youths", "Boys" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "SubCategoryID", "CreatedDate", "SubCategoryDescription", "SubCategoryName" },
                values: new object[,]
                {
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Girls youths", "Girls" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Boys infant products", "Boys Infant" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Girls infant products", "Girls Infant" }
                });
        }
    }
}
