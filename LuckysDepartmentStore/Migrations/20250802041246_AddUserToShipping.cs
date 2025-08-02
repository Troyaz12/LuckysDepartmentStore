using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressID",
                table: "CustomerOrders",
                newName: "ShippingID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_ShippingAddressID",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_ShippingID");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Shipping",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12aeb4e7-4700-4e0e-865a-4b7f3b745213", "AQAAAAIAAYagAAAAEGAheg4M2nmpfzvLhJOTykdY7hV5i6Y7NsHgK/z0BGfgLf4Gbbu320jPM9Ns+FfZMw==", "9dffb85d-4572-465b-a417-ac40c5d673f2" });

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_UserId",
                table: "Shipping",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Shipping_ShippingID",
                table: "CustomerOrders",
                column: "ShippingID",
                principalTable: "Shipping",
                principalColumn: "ShippingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_AspNetUsers_UserId",
                table: "Shipping",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Shipping_ShippingID",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_AspNetUsers_UserId",
                table: "Shipping");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_UserId",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shipping");

            migrationBuilder.RenameColumn(
                name: "ShippingID",
                table: "CustomerOrders",
                newName: "ShippingAddressID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_ShippingID",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_ShippingAddressID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4295247-bd6c-4e5e-91ee-26e9e307c54e", "AQAAAAIAAYagAAAAEHShHLcBD1irhh6mMwJS6J+DsgoElW907nUPcYqD8rb65skcu1BXJGeS4WJlXV68BQ==", "4b9da987-3c98-4c17-b84b-c7f85fb303f4" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ShippingAddress_ShippingAddressID",
                table: "CustomerOrders",
                column: "ShippingAddressID",
                principalTable: "ShippingAddress",
                principalColumn: "ShippingAddressID");
        }
    }
}
