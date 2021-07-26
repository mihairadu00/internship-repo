using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore_DataPersistence.Migrations
{
    public partial class ItemListingDateStringAttributeDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingDate",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingDate",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
