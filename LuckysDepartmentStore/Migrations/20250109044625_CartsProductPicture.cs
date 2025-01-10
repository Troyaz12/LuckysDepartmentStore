using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class CartsProductPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProductPicture",
                table: "Carts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c626fe90-b078-4d1b-98d4-cdc02ec241a9", "AQAAAAIAAYagAAAAEAZ+msL5SekOUX2qXS9MgDxSPRtKXACpFYqdguVh8yrQd47+a5iVmnwMCkNiKUnqzg==", "7320df2e-20b0-4f7c-97b8-9c907c400fe2" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(6297));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(6344));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(7485));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 729, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(599));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(613));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(615));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(616));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(618));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 22, 46, 24, 730, DateTimeKind.Local).AddTicks(619));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders",
                column: "ShippingAddressID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c853d635-74bb-40c2-b80f-3612f87fad87", "AQAAAAIAAYagAAAAEG1AcrIiDpUJsgAmUKSL+9ZWjxqyqHtKbpV/pE27C8zLiRn9b6/VY/qNtmvrMXAI6Q==", "7a0cfeba-2b7e-44bb-b177-67bb72415ee4" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(2652));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(2706));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(2708));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(3967));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(3969));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7055));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7058));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7060));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 54, 59, 258, DateTimeKind.Local).AddTicks(7061));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders",
                column: "ShippingAddressID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
