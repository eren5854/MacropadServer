using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacropadServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class macropaddevice_macropadmodel_macropadinput_macropadeyeanimation_appuser_siniflari_olusturuldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MacropadModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ModelSerialNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModelVersion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ModelDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DeviceSupport = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ButtonCount = table.Column<int>(type: "integer", nullable: false),
                    ModCount = table.Column<int>(type: "integer", nullable: false),
                    IsScreenExist = table.Column<bool>(type: "boolean", nullable: false),
                    ScreenType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ScreenSize = table.Column<double>(type: "double precision", nullable: true),
                    ConnectionType = table.Column<int>(type: "integer", nullable: true),
                    MicrocontrollerType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PowerType = table.Column<int>(type: "integer", nullable: true),
                    Rechargeable = table.Column<bool>(type: "boolean", nullable: true),
                    CaseColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CaseMaterial = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CaseDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacropadModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    SecretToken = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ForgotPasswordCode = table.Column<int>(type: "integer", nullable: true),
                    ForgotPasswordCodeSendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmailConfirmCode = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacropadDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MacropadName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MacropadSecretToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsEyeAnimationEnabled = table.Column<bool>(type: "boolean", nullable: true),
                    MacropadModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacropadDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacropadDevices_MacropadModels_MacropadModelId",
                        column: x => x.MacropadModelId,
                        principalTable: "MacropadModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MacropadDevices_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MacropadEyeAnimations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EyeAnimationType = table.Column<int>(type: "integer", nullable: false),
                    EyeAnimationTrigger = table.Column<int>(type: "integer", nullable: false),
                    MacropadDeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacropadEyeAnimations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacropadEyeAnimations_MacropadDevices_MacropadDeviceId",
                        column: x => x.MacropadDeviceId,
                        principalTable: "MacropadDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MacropadInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InputName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    InputIndex = table.Column<int>(type: "integer", nullable: true),
                    ModIndex = table.Column<int>(type: "integer", nullable: true),
                    InputBitMap = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    InputType = table.Column<int>(type: "integer", nullable: true),
                    Item1 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Item2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Item3 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Item4 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MacropadDeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacropadInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacropadInputs_MacropadDevices_MacropadDeviceId",
                        column: x => x.MacropadDeviceId,
                        principalTable: "MacropadDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MacropadDevices_AppUserId",
                table: "MacropadDevices",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MacropadDevices_MacropadModelId",
                table: "MacropadDevices",
                column: "MacropadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MacropadEyeAnimations_MacropadDeviceId",
                table: "MacropadEyeAnimations",
                column: "MacropadDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MacropadInputs_MacropadDeviceId",
                table: "MacropadInputs",
                column: "MacropadDeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MacropadEyeAnimations");

            migrationBuilder.DropTable(
                name: "MacropadInputs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "MacropadDevices");

            migrationBuilder.DropTable(
                name: "MacropadModels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
