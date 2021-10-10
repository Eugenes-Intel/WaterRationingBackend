using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterRationingBackend.DataAccess.Migrations
{
    public partial class SetNamepropertyforCityandSuburbtoUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Suburbs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Suburbs");
        }
    }
}
