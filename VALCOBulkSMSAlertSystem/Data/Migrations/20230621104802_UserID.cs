using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VALCOBulkSMSAlertSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AspnetUsers",
                table: "Messages",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Messages",
                newName: "AspnetUsers");
        }
    }
}
