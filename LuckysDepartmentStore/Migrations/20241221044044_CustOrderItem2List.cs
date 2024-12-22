using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class CustOrderItem2List : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerOrderItemId",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "163c88c9-a2db-4384-9f0e-e09bfe7bba52", "AQAAAAIAAYagAAAAECnvncfmjPCoB42jlaUuK21VMGWb2/vL38eaPZTgOU/hV1doouXJqvFMhPahBaLkrg==", "d30f8055-4ce9-45c5-b28f-b1188c02a859" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(8334));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 54, DateTimeKind.Local).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2888));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 22, 40, 44, 55, DateTimeKind.Local).AddTicks(2894));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerOrderItemId",
                table: "Shipping",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
