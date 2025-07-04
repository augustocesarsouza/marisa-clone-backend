using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyTableProductCommentLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                table: "tb_product_comment_likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "reaction",
                table: "tb_product_comment_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_comment_likes_product_id",
                table: "tb_product_comment_likes",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_product_comment_likes_tb_marisa_products_product_id",
                table: "tb_product_comment_likes",
                column: "product_id",
                principalTable: "tb_marisa_products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_product_comment_likes_tb_marisa_products_product_id",
                table: "tb_product_comment_likes");

            migrationBuilder.DropIndex(
                name: "IX_tb_product_comment_likes_product_id",
                table: "tb_product_comment_likes");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "tb_product_comment_likes");

            migrationBuilder.DropColumn(
                name: "reaction",
                table: "tb_product_comment_likes");
        }
    }
}
