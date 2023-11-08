using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GptWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddFaqsTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceUrl",
                table: "Faqs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceUrl",
                table: "Faqs");
        }
    }
}
