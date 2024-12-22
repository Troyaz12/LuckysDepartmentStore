using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class CustOrderItemShipid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrderItemId",
                table: "Shipping");

            migrationBuilder.AddColumn<int>(
                name: "ShippingID",
                table: "CustomerOrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0942d3d1-0d2c-41d9-b13f-6246fc301266", "AQAAAAIAAYagAAAAEDBdoEyLn/MTykluI4+ERdk0fQS9YyBqUkr0YoNJ5MnSZKYgDIp4SB+kYjaduw0jTA==", "497e7717-9326-441b-820c-7b74b3c6f902" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3913));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3963));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(3964));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5056));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(7999));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 23, 4, 34, 107, DateTimeKind.Local).AddTicks(8001));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingID",
                table: "CustomerOrderItems");

            migrationBuilder.AddColumn<string>(
                name: "CustomerOrderItemId",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
