using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class SizeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizesID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c05a190-e2a8-4698-a122-4baa534ba263", "AQAAAAIAAYagAAAAEMGyssS+4qwQnHO4AaE79pdD5wU/CehrfD52Eu49s0GDN7W+ZleUVzyxkqEoDfqsqw==", "662c2d6b-1293-47a4-b8bf-3fe2735f557d" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2702));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5607));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 16, 16, 36, 22, 561, DateTimeKind.Local).AddTicks(5628));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sizes");

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
    }
}
