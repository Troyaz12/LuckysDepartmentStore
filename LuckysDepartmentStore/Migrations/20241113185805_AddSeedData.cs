using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7989d3e-b752-4799-83d5-a9a10dee970a", "AQAAAAIAAYagAAAAEAzjyHxF0CYuZ+BtJv2Xwjy7C6uhzMSNwA/HgxH1HD8EjG6Zzlh92vQgbJ221e3NPw==", "fce3ff65-2399-4cc1-983a-fd61fdd5f084" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryDescription", "CategoryName", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Jeans", "Jeans", new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6927) },
                    { 2, "Shirts", "Shirts", new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6970) },
                    { 3, "Shoes", "Shoes", new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(6972) }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorID", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8081), "Red" },
                    { 2, new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8091), "Green" },
                    { 3, new DateTime(2024, 11, 13, 12, 58, 4, 238, DateTimeKind.Local).AddTicks(8093), "Blue" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "SubCategoryID", "CreatedDate", "SubCategoryDescription", "SubCategoryName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1260), "Mens products.", "Mens" },
                    { 2, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1274), "Womens products.", "Womens" },
                    { 3, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1276), "Boys youths", "Boys" },
                    { 4, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1277), "Girls youths", "Girls" },
                    { 5, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1279), "Boys infant products", "Boys Infant" },
                    { 6, new DateTime(2024, 11, 13, 12, 58, 4, 239, DateTimeKind.Local).AddTicks(1280), "Girls infant products", "Girls Infant" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3);

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f68b251-7cc8-4adc-b90c-38073f46530a", "AQAAAAIAAYagAAAAEDmjzhO9T6Fgn4on/T4mHgnf5awpm6zoUD+vG0c6Ump8o2lJyDj2WyehM7mMhnIl7Q==", "ff4068f1-f9d1-4e93-b59f-82dd87999a55" });
        }
    }
}
