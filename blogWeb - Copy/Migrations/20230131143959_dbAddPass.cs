using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogWeb.Migrations
{
    /// <inheritdoc />
    public partial class dbAddPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tbl_Profile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tbl_Profile");
        }
    }
}
