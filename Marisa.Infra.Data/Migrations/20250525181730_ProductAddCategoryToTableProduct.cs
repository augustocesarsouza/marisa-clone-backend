using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductAddCategoryToTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "tb_marisa_products",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "Moda Feminina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "tb_marisa_products");
        }
    }
}
