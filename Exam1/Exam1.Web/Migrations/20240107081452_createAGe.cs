using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam1.Web.Migrations
{
    /// <inheritdoc />
    public partial class createAGe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Age",
                table: "NIDs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "NIDs");
        }
    }
}
