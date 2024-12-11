using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeoSoft.Migrations
{
    /// <inheritdoc />
    public partial class NaranjaConLimon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "DetallesPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetallesPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "subtotal",
                table: "DetallesPedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "DetallesPedidos");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetallesPedidos");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "DetallesPedidos");
        }
    }
}
