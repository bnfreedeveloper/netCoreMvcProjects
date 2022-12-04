using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efCoreRelationships.Migrations
{
    public partial class onetomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Products",
                newName: "categId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                newName: "IX_Products_categId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categId",
                table: "Products",
                column: "categId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "categId",
                table: "Products",
                newName: "Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categId",
                table: "Products",
                newName: "IX_Products_Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
