using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProductAdditionalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_product_additional_info",
                columns: table => new
                {
                    product_additional_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    imgs_secondary = table.Column<List<string>>(type: "text[]", nullable: false),
                    about_product = table.Column<string>(type: "jsonb", nullable: false),
                    composition = table.Column<string>(type: "text", nullable: false),
                    shipping_information = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_additional_info", x => x.product_additional_info_id);
                    table.ForeignKey(
                        name: "FK_tb_product_additional_info_tb_marisa_products_product_id",
                        column: x => x.product_id,
                        principalTable: "tb_marisa_products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_additional_info_product_id",
                table: "tb_product_additional_info",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_product_additional_info");
        }
    }
}
