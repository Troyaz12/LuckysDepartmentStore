using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class PaymentOptAddUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentOptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(7900));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(8994));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(9006));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 356, DateTimeKind.Local).AddTicks(9008));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbd55417-ba37-4381-a21a-c5172f016222", "AQAAAAIAAYagAAAAEFWHD0jeSKePTSBYz2cmmfg2ccCJK6ruP8b3JBXMsnfgSyn9f1AjrGRwDKzOThqhiA==", "853ab358-7501-43fd-a9f0-b637d0183bdb" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1986));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1989));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 16, 46, 35, 357, DateTimeKind.Local).AddTicks(1992));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptions_UserId",
                table: "PaymentOptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_AspNetUsers_UserId",
                table: "PaymentOptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_AspNetUsers_UserId",
                table: "PaymentOptions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentOptions_UserId",
                table: "PaymentOptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentOptions");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(3038));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20f758e7-7642-4b1e-8f44-e7a598209aaa", "AQAAAAIAAYagAAAAEItdo00EwaRl/QyQ1gBHHLMa2UzP+lwp0I3ORZb0EBdFeM3VV4jKl2SCi4013SRpxA==", "f456db44-5b64-417b-813e-e51cbae47cda" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7651));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7652));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 14, 43, 38, 924, DateTimeKind.Local).AddTicks(7655));
        }
    }
}
