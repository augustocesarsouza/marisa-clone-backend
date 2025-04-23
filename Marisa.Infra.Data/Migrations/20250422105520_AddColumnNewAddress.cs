using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNewAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_user",
                table: "tb_address");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_address",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "main_address",
                table: "tb_address",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_address_user",
                table: "tb_address",
                column: "user_id",
                principalTable: "tb_marisa_users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_user",
                table: "tb_address");

            migrationBuilder.DropColumn(
                name: "main_address",
                table: "tb_address");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_address",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_address_user",
                table: "tb_address",
                column: "user_id",
                principalTable: "tb_marisa_users",
                principalColumn: "user_id");
        }
    }
}
