using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerNavProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04782273-ff6f-4ad5-a60e-05d317aa3c81", "AQAAAAIAAYagAAAAEFnL9+QrJNyIcXGSBJxCmkn1nNnVQnuKFn+mOTYQNzaJARiqtYN8/cAXnsSK52ONtw==", "10434618-3c28-420b-9ecc-e16c48d7e0fc" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(911));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(913));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(2043));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(2045));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5437));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5440));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5442));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 21, 54, 938, DateTimeKind.Local).AddTicks(5444));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerID",
                table: "CustomerOrders",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerID",
                table: "CustomerOrders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerID",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_CustomerID",
                table: "CustomerOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c38bb80e-65fb-4269-a1e0-2f52f5c65dfd", "AQAAAAIAAYagAAAAEHqPK4o5s3+Qa/5VCZvi/G9F2G0Nz+GqHialpd8bCd8ugHnOO5WrMdMFrF7GK7Z6yg==", "b4192805-3450-4f11-bbbb-ba85fa3b0389" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(8597));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(8643));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(8645));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(9714));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 714, DateTimeKind.Local).AddTicks(9716));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2735));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2755));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 22, 2, 37, 715, DateTimeKind.Local).AddTicks(2756));
        }
    }
}
