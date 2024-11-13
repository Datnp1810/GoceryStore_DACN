using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoceryStore_DACN.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldUserHoaDons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HoaDons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_UserId",
                table: "HoaDons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_AspNetUsers_UserId",
                table: "HoaDons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_AspNetUsers_UserId",
                table: "HoaDons");

            migrationBuilder.DropIndex(
                name: "IX_HoaDons_UserId",
                table: "HoaDons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HoaDons");
        }
    }
}
