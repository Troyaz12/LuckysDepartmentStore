using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuckysDepartmentStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6770720c-9f8f-4525-9241-c05a8ac4f861", null, "Customer", "CUSTOMER" },
                    { "720cd9c8-9418-4305-b2b9-c42017f5c8e4", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d601656b-5848-4236-96d5-d722d471089d", 0, "5f68b251-7cc8-4adc-b90c-38073f46530a", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDmjzhO9T6Fgn4on/T4mHgnf5awpm6zoUD+vG0c6Ump8o2lJyDj2WyehM7mMhnIl7Q==", null, false, "ff4068f1-f9d1-4e93-b59f-82dd87999a55", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "720cd9c8-9418-4305-b2b9-c42017f5c8e4", "d601656b-5848-4236-96d5-d722d471089d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6770720c-9f8f-4525-9241-c05a8ac4f861");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "720cd9c8-9418-4305-b2b9-c42017f5c8e4", "d601656b-5848-4236-96d5-d722d471089d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "720cd9c8-9418-4305-b2b9-c42017f5c8e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d601656b-5848-4236-96d5-d722d471089d");

          
        }
    }
}
