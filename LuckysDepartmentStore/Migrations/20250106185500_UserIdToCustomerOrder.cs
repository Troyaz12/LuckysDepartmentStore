using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class UserIdToCustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_UserId",
                table: "CustomerOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_AspNetUsers_UserId",
                table: "CustomerOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_AspNetUsers_UserId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_UserId",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e831494-9f30-4249-84f2-3739bcadae2c", "AQAAAAIAAYagAAAAEFldiTE1n1/MliONlZi5AfC2ew8jU+SK5q5D6syiE08xziiGJcQtdNK+raN4U5YLnQ==", "705c16d5-6dd1-4fa7-8a05-3ece84119da1" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(5672));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(5674));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(6749));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9569));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9571));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 6, 12, 10, 50, 518, DateTimeKind.Local).AddTicks(9575));

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
