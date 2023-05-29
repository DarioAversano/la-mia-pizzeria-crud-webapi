using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMiaPizzeria.Migrations
{
    /// <inheritdoc />
    public partial class PizzaCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaCategoryId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "pizzaCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzaCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_PizzaCategoryId",
                table: "Pizza",
                column: "PizzaCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_pizzaCategories_PizzaCategoryId",
                table: "Pizza",
                column: "PizzaCategoryId",
                principalTable: "pizzaCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_pizzaCategories_PizzaCategoryId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "pizzaCategories");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_PizzaCategoryId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "PizzaCategoryId",
                table: "Pizza");
        }
    }
}
