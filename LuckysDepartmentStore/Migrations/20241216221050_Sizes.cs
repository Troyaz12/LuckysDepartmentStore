using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class Sizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeID",
                table: "ColorProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51244486-1e67-41cd-8eb9-ad5d6d2af95c", "AQAAAAIAAYagAAAAEOLQzeaYm6qKBJDEixiyiPKscDZLfiIqw+Ceqgf/AXAuJgHthqeBCxLJI3Y3yJsydQ==", "01e5ed42-bb3b-479e-9ce6-307ee1c87c18" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 183, DateTimeKind.Local).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 183, DateTimeKind.Local).AddTicks(9553));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 183, DateTimeKind.Local).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(654));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3661));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3676));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3681));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 10, 49, 184, DateTimeKind.Local).AddTicks(3682));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeID",
                table: "ColorProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75cea062-e04c-48f4-9621-ca5dba11d3a9", "AQAAAAIAAYagAAAAEN62Zym5NtjObBprbGTUskxk+St6oBwEULRRB30DAjrR3mUaB0x9eOvWUGGxeT7rmQ==", "cfe9a9cb-d09c-41a6-b440-fef80a90629a" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8186));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8202));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8204));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 11, 16, 28, 38, 419, DateTimeKind.Local).AddTicks(8207));
        }
    }
}
