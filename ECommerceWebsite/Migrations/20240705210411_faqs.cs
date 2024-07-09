using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class faqs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_faqs",
                columns: table => new
                {
                    faqs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    faqs_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faqs_message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_faqs", x => x.faqs_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_faqs");
        }
    }
}
