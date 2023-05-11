using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserForPasswordEncryption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("password", "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "password",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: "Pass@123"
                // oldClrType: typeof(string),
                // oldType: "nvarchar(max)"
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordKey",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordKey",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
