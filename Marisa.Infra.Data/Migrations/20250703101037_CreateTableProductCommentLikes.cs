using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProductCommentLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_comment_likes",
                columns: table => new
                {
                    product_comment_likes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_comment_likes", x => x.product_comment_likes_id);
                    table.ForeignKey(
                        name: "FK_tb_product_comment_likes_tb_marisa_users_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_marisa_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_product_comment_likes_tb_product_comments_product_commen~",
                        column: x => x.product_comment_id,
                        principalTable: "tb_product_comments",
                        principalColumn: "product_comments_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_comment_likes_product_comment_id",
                table: "tb_product_comment_likes",
                column: "product_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_comment_likes_user_id",
                table: "tb_product_comment_likes",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_comment_likes");
        }
    }
}
