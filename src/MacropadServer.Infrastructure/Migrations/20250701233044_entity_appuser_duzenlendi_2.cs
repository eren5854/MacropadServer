using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacropadServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entity_appuser_duzenlendi_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteByUserId",
                table: "Users",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "MacropadModels",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteByUserId",
                table: "MacropadModels",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MacropadModels",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "MacropadInputs",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteByUserId",
                table: "MacropadInputs",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MacropadInputs",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "MacropadEyeAnimations",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteByUserId",
                table: "MacropadEyeAnimations",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MacropadEyeAnimations",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "MacropadDevices",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteByUserId",
                table: "MacropadDevices",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MacropadDevices",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Users",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "Users",
                newName: "DeleteByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "MacropadModels",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "MacropadModels",
                newName: "DeleteByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MacropadModels",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "MacropadInputs",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "MacropadInputs",
                newName: "DeleteByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MacropadInputs",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "MacropadEyeAnimations",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "MacropadEyeAnimations",
                newName: "DeleteByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MacropadEyeAnimations",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "MacropadDevices",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "MacropadDevices",
                newName: "DeleteByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MacropadDevices",
                newName: "CreatedByUserId");
        }
    }
}
