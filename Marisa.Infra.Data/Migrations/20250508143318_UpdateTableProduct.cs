using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_marisa_products_tb_marisa_users_user_id",
                table: "tb_marisa_products");

            migrationBuilder.DropIndex(
                name: "ix_product_user_id",
                table: "tb_marisa_products");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tb_marisa_products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tb_marisa_products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_user_id",
                table: "tb_marisa_products",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_marisa_products_tb_marisa_users_user_id",
                table: "tb_marisa_products",
                column: "user_id",
                principalTable: "tb_marisa_users",
                principalColumn: "user_id");
        }
    }
}
