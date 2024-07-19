using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class updateproductandcategorymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product",
                column: "cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_product_tbl_categories_cat_id",
                table: "tbl_product",
                column: "cat_id",
                principalTable: "tbl_categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_product_tbl_categories_cat_id",
                table: "tbl_product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product");
        }
    }
}
