using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSWeb.Migrations
{
    /// <inheritdoc />
    public partial class new4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "InventoryItems",
                newName: "Qty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "InventoryItems",
                newName: "IsAvailable");
        }
    }
}
