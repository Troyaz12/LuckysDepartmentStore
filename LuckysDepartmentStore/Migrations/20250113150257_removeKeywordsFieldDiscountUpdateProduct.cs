using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class removeKeywordsFieldDiscountUpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Discounts");

            migrationBuilder.AddColumn<string>(
                name: "DiscountTag",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c6db61-86aa-4a45-998b-b08d42d85aad", "AQAAAAIAAYagAAAAEAdk6H4YEbyIJLN9kNfYuvVpFBUMIWE856g1ouMN3n3fHAiG5FDP7N8xX3rvPCmnSw==", "a9f2cf3d-6e96-434f-8849-23d730751d12" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(288));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(291));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4605));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 13, 9, 2, 56, 567, DateTimeKind.Local).AddTicks(4610));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountTag",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
