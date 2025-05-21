using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_marisa_products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    price_discounted = table.Column<double>(type: "double precision", nullable: false),
                    discount_percentage = table.Column<int>(type: "integer", nullable: false),
                    installment_price = table.Column<double>(type: "double precision", nullable: false),
                    installment_times_marisa_card = table.Column<int>(type: "integer", nullable: false),
                    installment_times_credit_card = table.Column<int>(type: "integer", nullable: false),
                    colors = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    sizes_available = table.Column<List<string>>(type: "varchar[]", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    quantity_available = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_tb_marisa_products_tb_marisa_users_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_marisa_users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_user_id",
                table: "tb_marisa_products",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_marisa_products");
        }
    }
}
