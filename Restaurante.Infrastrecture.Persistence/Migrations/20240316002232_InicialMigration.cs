using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Infrastrecture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.IdIngrediente);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    IdMesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadPersona = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoMesa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.IdMesa);
                });

            migrationBuilder.CreateTable(
                name: "Platos",
                columns: table => new
                {
                    IdPlato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Personas = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaIngredientes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platos", x => x.IdPlato);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstadoOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdMesa = table.Column<int>(type: "int", nullable: false),
                    ListaPlatos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_Ordenes_Mesas_IdMesa",
                        column: x => x.IdMesa,
                        principalTable: "Mesas",
                        principalColumn: "IdMesa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatoIngrediente",
                columns: table => new
                {
                    IngredientesIdIngrediente = table.Column<int>(type: "int", nullable: false),
                    PlatosIdPlato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatoIngrediente", x => new { x.IngredientesIdIngrediente, x.PlatosIdPlato });
                    table.ForeignKey(
                        name: "FK_PlatoIngrediente_Ingredientes_IngredientesIdIngrediente",
                        column: x => x.IngredientesIdIngrediente,
                        principalTable: "Ingredientes",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatoIngrediente_Platos_PlatosIdPlato",
                        column: x => x.PlatosIdPlato,
                        principalTable: "Platos",
                        principalColumn: "IdPlato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenPlato",
                columns: table => new
                {
                    OrdenesIdOrden = table.Column<int>(type: "int", nullable: false),
                    PlatosIdPlato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenPlato", x => new { x.OrdenesIdOrden, x.PlatosIdPlato });
                    table.ForeignKey(
                        name: "FK_OrdenPlato_Ordenes_OrdenesIdOrden",
                        column: x => x.OrdenesIdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenPlato_Platos_PlatosIdPlato",
                        column: x => x.PlatosIdPlato,
                        principalTable: "Platos",
                        principalColumn: "IdPlato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdMesa",
                table: "Ordenes",
                column: "IdMesa");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPlato_PlatosIdPlato",
                table: "OrdenPlato",
                column: "PlatosIdPlato");

            migrationBuilder.CreateIndex(
                name: "IX_PlatoIngrediente_PlatosIdPlato",
                table: "PlatoIngrediente",
                column: "PlatosIdPlato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenPlato");

            migrationBuilder.DropTable(
                name: "PlatoIngrediente");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Platos");

            migrationBuilder.DropTable(
                name: "Mesas");
        }
    }
}
