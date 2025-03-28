using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTbUsersToTbMarisaUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tb_users",
                newName: "tb_marisa_users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tb_marisa_users",
                newName: "tb_users");
        }
    }
}
