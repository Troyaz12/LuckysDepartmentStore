using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class addSizeIntColorChangeCarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "604d6912-94d6-4300-8799-df6f432831f2", "AQAAAAIAAYagAAAAEJ1EQw/0lItLukf/Kj0K59nfyR61vQrMKqKCvBsnFWnizzlyryfcFyVxLeEA0WzoPQ==", "9d77a96f-529f-4eb5-9bac-ba4246a6cac8" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(6269));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 868, DateTimeKind.Local).AddTicks(7713));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(534));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(546));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 53, 57, 869, DateTimeKind.Local).AddTicks(556));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea141593-56e9-4081-a74d-294236d0fc53", "AQAAAAIAAYagAAAAELVykYuEYYKoxEvYLBfRsMu5YywP4NJr+qW6i4J49W20n24E455Q0SzWWi9LUu/tow==", "752080b8-0fab-496f-89a3-103d0f296735" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(2677));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 334, DateTimeKind.Local).AddTicks(5229));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1529));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1544));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 20, 0, 39, 32, 335, DateTimeKind.Local).AddTicks(1563));
        }
    }
}
