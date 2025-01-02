using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class UsernameCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CustomerOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0dd425cd-9b36-4f08-9b6c-6e41f4e3ca62", "AQAAAAIAAYagAAAAEA3FshvfnD/A2iGUBsG8tvnb8ZLMNX9X4lJqoznDu8YXRZ+3GKc8udvXNlLkz+JV+Q==", "57465bc6-b370-4077-ac7e-ca5714f22f0e" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4878));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4880));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 30, 21, 45, 22, 480, DateTimeKind.Local).AddTicks(4881));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CustomerOrders");

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
    }
}
