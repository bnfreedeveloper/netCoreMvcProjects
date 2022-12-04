using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efCoreRelationships.Migrations
{
    public partial class addproductsfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_Id",
                table: "Products");
        }
    }
}
