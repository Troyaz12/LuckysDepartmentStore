using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class updatedbsetNameConsumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerID",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "ShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "ShippingAddress");          

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "ShippingAddress",
                newName: "IX_ShippingAddress_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddress",
                table: "ShippingAddress",
                column: "ShippingAddressID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(402));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1555));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(1569));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbe5547b-a854-4e68-a931-4e6fb45cb73b", "AQAAAAIAAYagAAAAECd2+uQtf1pMR2ULpeA+eVpjjx3Y4eXhv5qXThGUw9O9bqyfejijpux3g3XTrOeltA==", "f7428026-16b2-495d-9c2f-3875d557daac" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4580));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4601));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 13, 21, 46, 932, DateTimeKind.Local).AddTicks(4602));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_CustomerID",
                table: "CustomerOrders",
                column: "CustomerID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_CustomerID",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddress_AspNetUsers_UserId",
                table: "ShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddress",
                table: "ShippingAddress");

            migrationBuilder.RenameTable(
                name: "ShippingAddress",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddress_UserId",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "ShippingAddressID");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(8955));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 452, DateTimeKind.Local).AddTicks(8967));

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "783f2cd9-8d35-4aba-92b8-790578cadfec", "AQAAAAIAAYagAAAAEGK9uiiL+uuxsDLaVAPrEq5cPHxbdRQ9v7hbsUvyfBgIG46pzUIE26Hsqrz49O7n8w==", "d18b9d40-6827-4d0f-b862-b7b6f20ae520" });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(1996));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(2011));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryID",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 3, 12, 46, 37, 453, DateTimeKind.Local).AddTicks(2018));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerID",
                table: "CustomerOrders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ShippingAddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

          
        }
    }
}
