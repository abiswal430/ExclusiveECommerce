using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExclusiveMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSaved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "Cart");
        }
    }
}
