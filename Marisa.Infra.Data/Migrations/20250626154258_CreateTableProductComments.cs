using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProductComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_comments",
                columns: table => new
                {
                    product_comments_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    star_quantity = table.Column<int>(type: "integer", nullable: false),
                    recommend_product = table.Column<bool>(type: "boolean", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    img_product = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_comments", x => x.product_comments_id);
                    table.ForeignKey(
                        name: "FK_tb_product_comments_tb_marisa_products_product_id",
                        column: x => x.product_id,
                        principalTable: "tb_marisa_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_comments_product_id",
                table: "tb_product_comments",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_comments");
        }
    }
}
