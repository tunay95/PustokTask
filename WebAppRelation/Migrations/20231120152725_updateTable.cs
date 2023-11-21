using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppRelation.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Brands_BrandId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandId",
                table: "Brands",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Brands_BrandId",
                table: "Brands",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
