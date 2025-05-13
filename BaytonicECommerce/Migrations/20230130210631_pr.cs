using Microsoft.EntityFrameworkCore.Migrations;

namespace BaytonicECommerce.Migrations
{
    public partial class pr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1454836a-64c2-44b0-ba16-47a301cad918");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1136424-100e-4d57-a78d-2da0fd2af872", "359b9385-1ebf-48f7-b7a6-acced9a609e3", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d1136424-100e-4d57-a78d-2da0fd2af872");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1454836a-64c2-44b0-ba16-47a301cad918", "d16dada6-76aa-46bb-9871-cb2cbc5b7cef", "Admin", "ADMIN" });
        }
    }
}
