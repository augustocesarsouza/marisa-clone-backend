using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddressChangeNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tb_address",
                newName: "tb_marisa_address");

            migrationBuilder.RenameIndex(
                name: "IX_tb_address_user_id",
                table: "tb_marisa_address",
                newName: "IX_tb_marisa_address_user_id");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "tb_marisa_products",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "nvarchar(150)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tb_marisa_address",
                newName: "tb_address");

            migrationBuilder.RenameIndex(
                name: "IX_tb_marisa_address_user_id",
                table: "tb_address",
                newName: "IX_tb_address_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "tb_marisa_products",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");
        }
    }
}
