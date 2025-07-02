using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacropadServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entity_appuser_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByUserId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "MacropadModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                table: "MacropadModels",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByUserId",
                table: "MacropadModels",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "MacropadModels",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "MacropadInputs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                table: "MacropadInputs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByUserId",
                table: "MacropadInputs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "MacropadInputs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "MacropadEyeAnimations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                table: "MacropadEyeAnimations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByUserId",
                table: "MacropadEyeAnimations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "MacropadEyeAnimations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "MacropadDevices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteAt",
                table: "MacropadDevices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByUserId",
                table: "MacropadDevices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "MacropadDevices",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MacropadModels");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "MacropadModels");

            migrationBuilder.DropColumn(
                name: "DeleteByUserId",
                table: "MacropadModels");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MacropadModels");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MacropadInputs");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "MacropadInputs");

            migrationBuilder.DropColumn(
                name: "DeleteByUserId",
                table: "MacropadInputs");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MacropadInputs");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MacropadEyeAnimations");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "MacropadEyeAnimations");

            migrationBuilder.DropColumn(
                name: "DeleteByUserId",
                table: "MacropadEyeAnimations");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MacropadEyeAnimations");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "MacropadDevices");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "MacropadDevices");

            migrationBuilder.DropColumn(
                name: "DeleteByUserId",
                table: "MacropadDevices");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MacropadDevices");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);
        }
    }
}
