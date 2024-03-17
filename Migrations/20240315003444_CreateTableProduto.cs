using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyMindsApi.Migrations;

/// <inheritdoc />
public partial class CreateTableProduto : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "produtos",
            columns: table => new
            {
                codigo = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                valorUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_produtos", x => x.codigo);
            })
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "produtos");
    }
}
