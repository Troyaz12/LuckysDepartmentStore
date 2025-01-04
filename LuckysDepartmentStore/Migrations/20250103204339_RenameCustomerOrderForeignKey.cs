using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class RenameCustomerOrderForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_CustomerID",
                table: "CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "CustomerOrders",
                newName: "ShippingAddressID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_CustomerID",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_ShippingAddressID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(3038));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20f758e7-7642-4b1e-8f44-e7a598209aaa", "AQAAAAIAAYagAAAAEItdo00EwaRl/QyQ1gBHHLMa2UzP+lwp0I3ORZb0EBdFeM3VV4jKl2SCi4013SRpxA==", "f456db44-5b64-417b-813e-e51cbae47cda" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7651));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7652));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7655));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders",
                column: "ShippingAddressID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressID",
                table: "CustomerOrders",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_ShippingAddressID",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_CustomerID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(402));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1555));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1569));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbe5547b-a854-4e68-a931-4e6fb45cb73b", "AQAAAAIAAYagAAAAECd2+uQtf1pMR2ULpeA+eVpjjx3Y4eXhv5qXThGUw9O9bqyfejijpux3g3XTrOeltA==", "f7428026-16b2-495d-9c2f-3875d557daac" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4580));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4601));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4602));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_CustomerID",
                table: "CustomerOrders",
                column: "CustomerID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
