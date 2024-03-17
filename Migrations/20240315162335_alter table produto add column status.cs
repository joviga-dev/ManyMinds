using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyMindsApi.Migrations
{
    /// <inheritdoc />
    public partial class altertableprodutoaddcolumnstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "produtos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "produtos");
        }
    }
}
