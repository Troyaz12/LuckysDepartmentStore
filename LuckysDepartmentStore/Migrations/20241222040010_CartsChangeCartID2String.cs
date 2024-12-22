using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class CartsChangeCartID2String : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CartID",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5db27669-55d0-4d5b-8a1e-e4f52b8fe7a5", "AQAAAAIAAYagAAAAEOy7g1odfLgPqqLh/88BCyrF08rhhyz5GPQJKxC0tBUYU2/q3HYSHp7376Sr7SekKw==", "94ca6c1c-4838-4525-bb1a-09da67f16f0e" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(3606));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(4744));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(4759));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7896));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 22, 0, 9, 302, DateTimeKind.Local).AddTicks(7902));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "Carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "636bcdb4-507d-4e52-9437-702c414e9d36", "AQAAAAIAAYagAAAAEEKSG5vC0X4epfY/1xUrC7pg1WkQRt4PaXL6jI8avBeRJDN+Gjf34VXbJvZfFHXKTA==", "30592f76-d112-474d-be1f-7656527af1c9" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 360, DateTimeKind.Local).AddTicks(9176));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 360, DateTimeKind.Local).AddTicks(9227));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 360, DateTimeKind.Local).AddTicks(9229));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(212));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(222));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3177));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3192));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3194));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3195));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3197));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 39, 33, 361, DateTimeKind.Local).AddTicks(3198));
        }
    }
}
