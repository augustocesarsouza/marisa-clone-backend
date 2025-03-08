using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marisa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatingNewPropertyTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                table: "tb_users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "cell_phone",
                table: "tb_users",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "tb_users",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<char>(
                name: "gender",
                table: "tb_users",
                type: "char(1)",
                nullable: false,
                defaultValue: 'M');

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "tb_users",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_image",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "cell_phone",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "telephone",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "user_image",
                table: "tb_users");
        }
    }
}
