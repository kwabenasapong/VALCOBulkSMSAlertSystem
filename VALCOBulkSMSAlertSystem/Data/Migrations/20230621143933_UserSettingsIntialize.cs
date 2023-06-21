using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VALCOBulkSMSAlertSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSettingsIntialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserSettingsId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSettingsId",
                table: "AspNetUsers",
                column: "UserSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers",
                column: "UserSettingsId",
                principalTable: "UserSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSettingsId",
                table: "AspNetUsers");
        }
    }
}
