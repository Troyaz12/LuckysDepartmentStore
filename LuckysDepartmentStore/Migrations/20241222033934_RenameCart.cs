using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class RenameCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ID);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f26a0d5d-5b45-4e69-b5a7-8d14a70da750", "AQAAAAIAAYagAAAAELi64HqGEjETFD/2kYM7iwzHhis2/F2yilIKTHBgS63GDNWEHVJJZwKGVNtnX4CFwQ==", "fd48a569-2d85-4393-846b-07eb22770283" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 272, DateTimeKind.Local).AddTicks(9693));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 272, DateTimeKind.Local).AddTicks(9754));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 272, DateTimeKind.Local).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(1013));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(1025));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4181));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4183));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4184));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4186));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 21, 21, 17, 17, 273, DateTimeKind.Local).AddTicks(4187));
        }
    }
}
