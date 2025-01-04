using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AppUserMakeShippingAddAList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingAddress_UserId",
                table: "ShippingAddress");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(914));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(2065));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(2076));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(2078));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70396786-3e00-410f-b305-efb45ee0025c", "AQAAAAIAAYagAAAAECl1kk7NwRK+Zyz3VU3CbpDCJ0rHmZmJUlmHWWkZxCqe0jgNQRYLldJWJtapTJbuMQ==", "a125e544-4946-4d46-abce-1bafcafc0c08" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5249));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5252));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5253));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 53, 29, 115, DateTimeKind.Local).AddTicks(5255));

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_UserId",
                table: "ShippingAddress",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingAddress_UserId",
                table: "ShippingAddress");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(6280));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(6283));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 161, DateTimeKind.Local).AddTicks(7287));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42190bec-bd94-42de-ab10-c0adb23800f2", "AQAAAAIAAYagAAAAELoPTZH8gerx+VwqvXqSPqPyK4afstzlIz/N0PnxdOzSCpekTwSuWhnwZO1yxqJPJw==", "54425ef8-ddb2-44f2-a752-a448549a2b66" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(305));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(307));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(308));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(310));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 48, 14, 162, DateTimeKind.Local).AddTicks(311));

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_UserId",
                table: "ShippingAddress",
                column: "UserId",
                unique: true);
        }
    }
}
