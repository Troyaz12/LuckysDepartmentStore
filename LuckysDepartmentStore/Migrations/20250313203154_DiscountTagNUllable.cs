using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class DiscountTagNUllable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscountTag",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c638049a-5ff9-480c-ad2e-6598258227ce", "AQAAAAIAAYagAAAAEOCzn49EvMUDYoiuqwT6fZliHXuz9K4XY92BIZ7ef1/1Io3woG7QMTIiOpvaTAsCPQ==", "de1e2573-5a01-4ca0-bee3-2dda3eeeb4f5" });

       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AlterColumn<string>(
                name: "DiscountTag",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "656c41c2-6999-4500-b3e6-cd762147f3c4", "AQAAAAIAAYagAAAAEHsZVc4PalCcjzl+vkB1SoeBbdGtCWZNKjjk2A0+R/Vvt6dai2f9aNsJJ6irqJLcnw==", "5f82fdd7-a3c7-4c76-ae93-d5f0b566e517" });
        }
    }
}
