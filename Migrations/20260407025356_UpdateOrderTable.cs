using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExclusiveMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Orders",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "TotalAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "ProductName");
        }
    }
}
