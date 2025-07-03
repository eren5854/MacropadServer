using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacropadServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class macropadmodel_sinifina_modelimage_eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelImage",
                table: "MacropadModels",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelImage",
                table: "MacropadModels");
        }
    }
}
