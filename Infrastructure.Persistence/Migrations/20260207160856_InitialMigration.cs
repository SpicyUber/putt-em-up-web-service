using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    FromPlayerID = table.Column<long>(type: "bigint", nullable: false),
                    ToPlayerID = table.Column<long>(type: "bigint", nullable: false),
                    SentTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reported = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => new { x.FromPlayerID, x.ToPlayerID, x.SentTimestamp });
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_FromPlayerID",
                        column: x => x.FromPlayerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ToPlayerID",
                        column: x => x.ToPlayerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchPerformances",
                columns: table => new
                {
                    PlayerID = table.Column<long>(type: "bigint", nullable: false),
                    MatchID = table.Column<long>(type: "bigint", nullable: false),
                    WonMatch = table.Column<bool>(type: "bit", nullable: false),
                    FinalScore = table.Column<int>(type: "int", nullable: false),
                    MMRDelta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPerformances", x => new { x.PlayerID, x.MatchID });
                    table.ForeignKey(
                        name: "FK_MatchPerformances_AspNetUsers_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPerformances_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, null, "user", "USER" },
                    { 2L, null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountDeleted", "AvatarFilePath", "ConcurrencyStamp", "Description", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1L, 0, false, "", "83d7c5aa-a2c1-4eb1-8eff-8179fec89dfe", "My Description.", "Aleksandar", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEC45QUvRYQCQR1Cd/ZnmLRpYcXeQc3OaA+WV/tGvooLPVXpZs+HfHvzuN/FqmNN8Og==", null, false, null, false, "admin" },
                    { 2L, 0, false, "magnus", "40dc99b7-9662-4897-8b40-fe0ea062b2df", "I am very good at this game.", "Magnus", null, false, false, null, null, "MAGNUS", "AQAAAAIAAYagAAAAEE0P/32ODt1bNSYlCGLizw+92utxCPfxn1g0wezUmLbyKfWqWZBYDCqqUSp6hAXHKA==", null, false, null, false, "magnus" },
                    { 3L, 0, false, "", "07fd0835-853a-4cc4-bbaf-b9aa6b971668", "I am very bad at this game.", "Bob", null, false, false, null, null, "BOB", "AQAAAAIAAYagAAAAEPBNCbm3wPX/Xa2X+i7WDIeahykRQHJEsVEuNaa3VKvMuLzjXDPTcJ6zKcSZbCMxKw==", null, false, null, false, "bob" }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchID", "Cancelled", "StartDate" },
                values: new object[,]
                {
                    { 1L, false, new DateTime(2012, 3, 3, 13, 20, 20, 0, DateTimeKind.Unspecified) },
                    { 2L, false, new DateTime(2022, 7, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 3L, false, new DateTime(2023, 7, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 4L, false, new DateTime(2023, 3, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 5L, false, new DateTime(2024, 12, 11, 12, 22, 33, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2L, 1L },
                    { 1L, 2L },
                    { 1L, 3L }
                });

            migrationBuilder.InsertData(
                table: "MatchPerformances",
                columns: new[] { "MatchID", "PlayerID", "FinalScore", "MMRDelta", "WonMatch" },
                values: new object[,]
                {
                    { 2L, 1L, 3, 100, true },
                    { 3L, 1L, 3, 100, true },
                    { 4L, 1L, 0, -100, false },
                    { 1L, 2L, 3, 100, true },
                    { 3L, 2L, 0, -100, false },
                    { 4L, 2L, 3, 100, true },
                    { 5L, 2L, 3, 100, true },
                    { 1L, 3L, 0, -100, false },
                    { 2L, 3L, 0, -100, false },
                    { 5L, 3L, 0, -100, false }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "FromPlayerID", "SentTimestamp", "ToPlayerID", "Content", "Reported" },
                values: new object[,]
                {
                    { 1L, new DateTime(2012, 3, 3, 1, 33, 12, 0, DateTimeKind.Unspecified), 2L, "ez", false },
                    { 1L, new DateTime(2022, 3, 3, 1, 34, 23, 0, DateTimeKind.Unspecified), 2L, "hello", false },
                    { 1L, new DateTime(2022, 3, 3, 1, 33, 12, 0, DateTimeKind.Unspecified), 3L, "it's been 10 years, lets play?", false },
                    { 2L, new DateTime(2012, 3, 3, 1, 34, 22, 0, DateTimeKind.Unspecified), 1L, ":(", false },
                    { 2L, new DateTime(2022, 3, 3, 1, 34, 22, 0, DateTimeKind.Unspecified), 1L, "How do I block you??", false },
                    { 2L, new DateTime(2022, 3, 3, 1, 33, 13, 0, DateTimeKind.Unspecified), 3L, "hi", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPerformances_MatchID",
                table: "MatchPerformances",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToPlayerID",
                table: "Messages",
                column: "ToPlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MatchPerformances");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
