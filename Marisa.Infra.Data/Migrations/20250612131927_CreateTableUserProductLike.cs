using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUserProductLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_user_product_likes",
                columns: table => new
                {
                    user_product_likes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    liked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_product_likes", x => x.user_product_likes_id);
                    table.ForeignKey(
                        name: "FK_tb_user_product_likes_tb_marisa_products_product_id",
                        column: x => x.product_id,
                        principalTable: "tb_marisa_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_user_product_likes_tb_marisa_users_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_marisa_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_product_likes_product_id",
                table: "tb_user_product_likes",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_product_likes_user_id",
                table: "tb_user_product_likes",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user_product_likes");
        }
    }
}
