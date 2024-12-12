using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class DateExpRenameDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateExpiration",
                table: "Discounts",
                newName: "ExpirationDate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75cea062-e04c-48f4-9621-ca5dba11d3a9", "AQAAAAIAAYagAAAAEN62Zym5NtjObBprbGTUskxk+St6oBwEULRRB30DAjrR3mUaB0x9eOvWUGGxeT7rmQ==", "cfe9a9cb-d09c-41a6-b440-fef80a90629a" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8186));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8202));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8204));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8207));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Discounts",
                newName: "DateExpiration");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a7d850a-0d06-466e-ae63-af1532b1aeaf", "AQAAAAIAAYagAAAAEGCIAoFpfdxN9mlKq8swBdwlOuwkpc6XSzRxZmfNeI3OLNkLdCSlBTB2vt8RX67/0A==", "25c67eeb-7c8c-45cd-82ea-f43cc081b3f8" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(3075));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(3129));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(4138));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7052));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7068));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7073));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 23, 55, 204, DateTimeKind.Local).AddTicks(7074));
        }
    }
}
