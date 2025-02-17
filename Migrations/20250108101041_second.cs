using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Login_and_Registration.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationStatus",
                table: "UserModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminId", "Password" },
                values: new object[] { "admin123", "adminPassword@123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: "admin123");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationStatus",
                table: "UserModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
