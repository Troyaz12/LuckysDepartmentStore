using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddPropCustOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "CustomerOrderItems",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "PaymentID",
                table: "CustomerOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CustomerOrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "246dd648-bd9d-4813-b64a-e7d5a24530c6", "AQAAAAIAAYagAAAAEC9SpauNXoAYEiYDdtuhuC5K04CEQDNV/s2JWV9kWUGCh4C4FqjY+z/H97QFwqLp3A==", "0d5da34c-fd19-4672-a170-17eb7aa16c4d" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(5142));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(6236));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(6249));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(6251));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9193));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9209));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9211));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9214));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 31, 9, 658, DateTimeKind.Local).AddTicks(9216));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomerOrderItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CustomerOrderItems",
                newName: "PaymentID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b5e4d95-4882-4f0f-91eb-a6b1323c6e79", "AQAAAAIAAYagAAAAEPqO4QEPydGoBvtgfXQQgwjVVSXlZxgALFfAmdKvcsaE2u82FPwvEpqMo0QxJobGaw==", "ca15acdc-fa4c-46bb-a3d7-dccbaca9965c" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8605));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8615));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 66, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1581));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1586));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 42, 52, 67, DateTimeKind.Local).AddTicks(1587));
        }
    }
}
