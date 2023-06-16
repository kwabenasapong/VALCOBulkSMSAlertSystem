using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VALCOBulkSMSAlertSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VALCOUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_VALCOUserId",
                table: "Messages",
                column: "VALCOUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_VALCOUserId",
                table: "Messages",
                column: "VALCOUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_VALCOUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_VALCOUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VALCOUserId",
                table: "Messages");
        }
    }
}
