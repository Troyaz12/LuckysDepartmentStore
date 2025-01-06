using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AccountRoutingNumsStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoutingNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoutingNumber",
                table: "PaymentOptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "PaymentOptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe46ff3d-0be7-42ef-b913-2fc6223e5d90", "AQAAAAIAAYagAAAAEG51e7kugeJB3Nm23dBI0vegy+hEehTEOItIMhdUekr3R0IsvSa7XGJ6QuESEtHCjA==", "b397e699-3e60-45db-94c3-58504ce70fbc" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(6287));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(6289));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(7289));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 742, DateTimeKind.Local).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(190));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(191));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(193));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 1, 50, 27, 743, DateTimeKind.Local).AddTicks(194));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoutingNumber",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RoutingNumber",
                table: "PaymentOptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "PaymentOptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f487186-d825-491a-b294-17e762e96669", "AQAAAAIAAYagAAAAEJU/O/aO5r6y+OMf2b4gPP4BPgEiFk+Qaatc7Zl2vIPLH/fKRtyasK4aKv3INfybvA==", "24a1fee3-3950-4d99-9a29-3a1c1353d7c9" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(2609));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(2664));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(3701));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6631));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6646));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6651));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 0, 26, 48, 883, DateTimeKind.Local).AddTicks(6653));
        }
    }
}
