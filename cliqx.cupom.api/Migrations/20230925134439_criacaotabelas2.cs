using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cliqx.cupom.api.Migrations
{
    /// <inheritdoc />
    public partial class criacaotabelas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoCupom",
                table: "cupom",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "cupom",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cupom_limite_cpf",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Cpf = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeCliente = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantidadeLimite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cupom_limite_cpf", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cupom_uso_pedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PedidoId = table.Column<long>(type: "bigint", nullable: false),
                    CupomId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cupom_uso_pedido", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "cupom",
                columns: new[] { "Id", "CodigoCupom", "DataAtualizacao", "DataCadastro", "DataValidade", "Descricao", "PercentualDesconto", "TipoCupom", "TipoDesconto", "Usuario", "ValorDesconto" },
                values: new object[,]
                {
                    { 1L, "PRIMEIRACOMPRA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 25, 10, 44, 39, 321, DateTimeKind.Local).AddTicks(3870), new DateTime(2028, 9, 25, 10, 44, 39, 321, DateTimeKind.Local).AddTicks(3852), "Destinado a usuários de primeira compra", 30m, 2, 2, "SNOG", 0m },
                    { 2L, "ANIVERSARIO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 25, 10, 44, 39, 321, DateTimeKind.Local).AddTicks(3874), new DateTime(2024, 9, 25, 10, 44, 39, 321, DateTimeKind.Local).AddTicks(3874), "Destinado a quem faz aniversário", 0m, 1, 1, "SNOG", 10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cupom_limite_cpf");

            migrationBuilder.DropTable(
                name: "cupom_uso_pedido");

            migrationBuilder.DeleteData(
                table: "cupom",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cupom",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "CodigoCupom",
                table: "cupom");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "cupom");
        }
    }
}
