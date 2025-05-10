using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JwtRefreshTokenisExpired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "JwtRefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "JwtRefreshTokens");
        }
    }
}
